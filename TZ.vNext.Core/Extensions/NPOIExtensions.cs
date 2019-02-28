//  -----------------------------------------------------------------------
//  <copyright file="NPOIExtensions.cs" company="TZEPM">
//      Copyright  TZEPM. All rights reserved.
//   </copyright>
//  <author>tzxx</author>
//  <date>2018-12-25</date>
//  -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TZ.vNext.Core.Attributes;
using TZ.vNext.Core.Const;
using TZ.vNext.Core.Schema;
using TZ.vNext.Core.Utility;

namespace TZ.vNext.Core.Extensions.NPOI
{
    public static class NpoiExtensions
    {
        private const int FontHeightInPoints = 11;
        private const int RowPageSize = 40000;

        /// <summary>
        /// 把集合转换为DataTable
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="items">源数据</param>
        /// <param name="selectedColumns">选择列</param>
        /// <returns>DataTable</returns>
        public static DataTable[] ToDataTable<T>(this IEnumerable<T> items, IList<string> selectedColumns)
        {
            return ToDataTable<T>(items, selectedColumns, true);
        }

        /// <summary>
        /// 把集合转换为DataTable(没有序号列)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="items">源数据</param>
        /// <param name="selectedColumns">选择列</param>
        /// <returns>DataTable</returns>
        public static DataTable[] ToDataTableNoSequenced<T>(this IEnumerable<T> items, IList<string> selectedColumns)
        {
            return ToDataTable<T>(items, selectedColumns, false);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="file">导入的文件</param>
        /// <returns>泛型集合</returns>
        public static IList<T> Import<T>(this IFormFile file) where T : class, new()
        {
            GuardUtils.NotNull(file, nameof(file));
            var workbook = new XSSFWorkbook(file.OpenReadStream());
            return workbook.GetSheetAt(0).Import<T>().ToList();
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sheet">sheet</param>
        /// <returns>泛型集合</returns>
        public static IList<T> Import<T>(this ISheet sheet) where T : class, new()
        {
            GuardUtils.NotNull(sheet, nameof(sheet));
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dic = new Dictionary<string, PropertyInfo>();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(ColumnNameAttribute), false);
                if (attributes.Length > 0)
                {
                    var columnNameAttribute = attributes[0] as ColumnNameAttribute;
                    if (columnNameAttribute != null)
                    {
                        dic.Add(columnNameAttribute.ColumnName, property);
                    }
                }
            }

            IRow row = sheet.GetRow(0);
            var dic1 = new Dictionary<int, PropertyInfo>();
            foreach (var cell in row.Cells)
            {
                var cellvalue = cell.StringCellValue;
                if (!string.IsNullOrEmpty(cellvalue))
                {
                    PropertyInfo pro = null;
                    if (dic.TryGetValue(cellvalue, out pro))
                    {
                        dic1.Add(cell.ColumnIndex, pro);
                    }
                }
            }

            var list = new List<T>();

            foreach (IRow r in sheet)
            {
                if (r.RowNum == 0)
                {
                    continue;
                }

                dynamic info = Row2Object<T>(r, dic1);
                info.RowNum = r.RowNum + 1;
                list.Add(info);
            }

            return list;
        }

        /// <summary>
        /// 导出HSSFWorkbook
        /// </summary>
        /// <param name="dts">DataTable数据</param>
        /// <param name="sheetName">sheet名称</param>
        /// <returns>返回HSSFWorkbook</returns>
        public static HSSFWorkbook ExportDynamic(this List<DataTable> dts, string sheetName)
        {
            return ExportDynamic(dts, sheetName, Array.Empty<ExportExcelAttribute>().ToList());
        }

        /// <summary>
        /// 导出HSSFWorkbook
        /// </summary>
        /// <param name="dts">DataTable数据</param>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="exportExcelTitleInfo">列头特性</param>
        /// <returns>返回HSSFWorkbook</returns>
        public static HSSFWorkbook ExportDynamic(this List<DataTable> dts, string sheetName, List<ExportExcelAttribute> exportExcelTitleInfo)
        {
            GuardUtils.NotNull(dts, nameof(dts));
            var sheetNames = new List<string>();
            for (int i = 0; i < dts.Count; i++)
            {
                if (i > 0)
                {
                    sheetNames.Add(sheetName + (i + 1));
                }
                else
                {
                    sheetNames.Add(sheetName);
                }
            }

            var book = ToHssfWorkbookDynamic(dts, sheetNames.ToArray(), exportExcelTitleInfo);
            return book;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="excelBookInfo">需要写入Excel的WorkBook</param>
        /// <returns>返回HSSFWorkbook</returns>
        public static HSSFWorkbook Export<T>(this ExcelWorkBookInfo excelBookInfo)
        {
            GuardUtils.NotNull(excelBookInfo, nameof(excelBookInfo));
            var book = ToHssfWorkbook<T>(excelBookInfo.Data, excelBookInfo.SheetName, excelBookInfo.ChoiseStr, excelBookInfo.IsShowSequenced ?? true);
            return book;
        }

        /// <summary>
        /// 文件字节
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="excelBookInfo">需要写入Excel的WorkBook</param>
        /// <returns>返回二进制数据</returns>
        public static byte[] ToBytes<T>(this ExcelWorkBookInfo excelBookInfo)
        {
            GuardUtils.NotNull(excelBookInfo, nameof(excelBookInfo));
            var book = ToHssfWorkbook<T>(excelBookInfo.Data, excelBookInfo.SheetName, excelBookInfo.ChoiseStr, excelBookInfo.IsShowSequenced ?? true);

            //文件字节
            using (var file = new MemoryStream())
            {
                book.Write(file);
                return file.ToArray();
            }
        }

        /// <summary>
        /// 构建Excel
        /// </summary>
        /// <param name="dt">Excel数据</param>
        /// <param name="workSheetName">Sheet名称</param>
        /// <param name="exportExcelTitleInfo">列头特性</param>
        /// <returns>返回Workbook</returns>
        public static HSSFWorkbook ToHssfWorkbookDynamic(List<DataTable> dt, string[] workSheetName, List<ExportExcelAttribute> exportExcelTitleInfo)
        {
            GuardUtils.NotNull(workSheetName, nameof(workSheetName));
            GuardUtils.NotNull(dt, nameof(dt));
            var book = new HSSFWorkbook();
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    ISheet sheet = book.CreateSheet(workSheetName[i]);
                    ////表头
                    SetTitle(book, sheet, dt[i], exportExcelTitleInfo, true, true);

                    ////数据行
                    SetDataRow(book, sheet, dt[i], true);
                }
            }

            return book;
        }

        /// <summary>
        /// 构建Excel
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dt">Excel数据</param>
        /// <param name="workSheetName">Sheet名称</param>
        /// <param name="selectColumnsStr">选择列</param>
        /// <param name="isShowSequenced">是否显示序号</param>
        /// <returns>返回Workbook</returns>
        public static HSSFWorkbook ToHssfWorkbook<T>(List<DataTable> dt, string[] workSheetName, string[] selectColumnsStr, bool isShowSequenced)
        {
            GuardUtils.NotNull(workSheetName, nameof(workSheetName));
            GuardUtils.NotNull(dt, nameof(dt));
            var book = new HSSFWorkbook();
            if (dt.Count > 0)
            {
                var exportAttributes = GetAttributesByClassName<T>((selectColumnsStr?[0]).Split(","));

                for (int i = 0; i < dt.Count; i++)
                {
                    ISheet sheet = book.CreateSheet(workSheetName[i]);

                    ////获取Excel每个Sheet中行列属性
                    if (dt.Select(x => x.TableName).Distinct().ToList().Count > 1)
                    {
                        exportAttributes = GetAttributesByClassName<T>((selectColumnsStr?[i]).Split(","));
                    }

                    ////表头
                    SetTitle(book, sheet, dt[i], exportAttributes, isShowSequenced);

                    ////数据行
                    SetDataRow(book, sheet, dt[i], isShowSequenced);
                }
            }

            return book;
        }

        /// <summary>
        /// 返回可以导出的字段
        /// </summary>
        /// <typeparam name="T">要查的类型</typeparam>
        /// <param name="t">泛型对象</param>
        /// <returns>属性,对应中文名称</returns>
        public static Dictionary<string, string> GetExportProperty<T>(this T t)
        {
            var propertyDictionary = new Dictionary<string, string>();
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                var descAttr = prop.GetCustomAttributes(typeof(ExportExcelAttribute), false);

                if (descAttr.Any())
                {
                    foreach (var attr in descAttr.Where(x => x is ExportExcelAttribute))
                    {
                        var exportExcelAttribute = attr as ExportExcelAttribute;
                        propertyDictionary.Add(exportExcelAttribute.ColunmName, prop.Name);
                    }
                }
            }

            return propertyDictionary;
        }

        /// <summary>
        /// 转换List为ExcelWorkBookInfo(全部列)
        /// </summary>
        /// <typeparam name="T">泛型定义</typeparam>
        /// <param name="list">泛型List</param>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>ExcelWorkBookInfo对象</returns>
        public static ExcelWorkBookInfo ToExcelWorkBook<T>(this IList<T> list, string sheetName, string fileName)
        {
            var dts = new List<DataTable>();
            dts.AddRange(ToDataTable(list, new string[] { }));
            return ConverteExcelWorkBook(dts, sheetName, fileName, string.Empty);
        }

        /// <summary>
        /// 转换List为ExcelWorkBookInfo
        /// </summary>
        /// <typeparam name="T">泛型定义</typeparam>
        /// <param name="list">泛型List</param>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="fileName">文件名</param>
        /// <param name="choiseStrs">选择列的字符串</param>
        /// <returns>ExcelWorkBookInfo对象</returns>
        public static ExcelWorkBookInfo ToExcelWorkBook<T>(this IList<T> list, string sheetName, string fileName, string[] choiseStrs)
        {
            var dts = new List<DataTable>();
            GuardUtils.NotNull(choiseStrs, nameof(choiseStrs));
            dts.AddRange(ToDataTable(list, choiseStrs[0].Split(",")));
            return ConverteExcelWorkBook(dts, sheetName, fileName, choiseStrs[0]);
        }

        /////// <summary>
        /////// 从文件转换为datatable
        /////// </summary>
        /////// <param name="path">文件路径</param>
        /////// <returns>DataTable</returns>
        ////public static DataTable ToDataTableFromFilePath(this string path)
        ////{
        ////    IWorkbook workbook;
        ////    using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
        ////    {
        ////        workbook = new HSSFWorkbook(file);
        ////    }

        ////    var sheet = workbook.GetSheetAt(0);
        ////    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

        ////    var dt = new DataTable();
        ////    for (int j = 0; j < 30; j++)
        ////    {
        ////        dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
        ////    }

        ////    while (rows.MoveNext())
        ////    {
        ////        IRow row = (HSSFRow)rows.Current;
        ////        DataRow dr = dt.NewRow();
        ////        ////var rowLength = row.LastCellNum;
        ////        for (int i = 0; i < 30; i++)
        ////        {
        ////            ICell cell = row.GetCell(i);

        ////            if (cell == null)
        ////            {
        ////                dr[i] = null;
        ////            }
        ////            else
        ////            {
        ////                dr[i] = cell.ToString();
        ////            }
        ////        }

        ////        dt.Rows.Add(dr);
        ////    }

        ////    return dt;
        ////}

        /// <summary>
        /// 必填及格式验证
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="models">泛型集合</param>
        /// <returns>验证结果</returns>
        public static IList<ExcelImportErroInfo> ValdiateModelList<T>(this IList<T> models)
        {
            var errors = new List<ExcelImportErroInfo>();
            var requied = Valdiate<T>();
            var needDataTypeValdiate = DataTypeValdiate<T>();
            if (models != null)
            {
                foreach (var info in models)
                {
                    dynamic originalInfo = info;
                    List<ExcelImportErroInfo> requiredErrors = GetRequiredError(requied, originalInfo);
                    if (requiredErrors.Any())
                    {
                        errors.AddRange(requiredErrors);
                    }

                    List<ExcelImportErroInfo> dataTypeErrors = GetDataTypeError<T>(needDataTypeValdiate, info, originalInfo);

                    if (dataTypeErrors.Any())
                    {
                        errors.AddRange(dataTypeErrors);
                    }
                }
            }

            return errors;
        }

        /// <summary>
        /// 必填及格式验证
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="models">泛型集合</param>
        /// <returns>验证结果</returns>
        public static List<string> ValidateModelList<T>(this IList<T> models)
        {
            var errors = models.ValdiateModelList();
            var messageList = new List<string>();
            foreach (var error in errors)
            {
                messageList.Add("第 " + error.Row + " 行数据异常：" + error.Message);
            }

            return messageList;
        }

        /// <summary>
        /// Sheet填充数据
        /// </summary>
        /// <param name="baseSheet">sheet</param>
        /// <param name="tbData">DataTable数据</param>
        public static void SetDataSourse(this ISheet baseSheet, DataTable tbData)
        {
            GuardUtils.NotNull(baseSheet, nameof(baseSheet));
            GuardUtils.NotNull(tbData, nameof(tbData));
            ////获取Title在DataTable数据中对应的列idx
            var dicData = GetTitleDic(tbData, baseSheet);
            for (int i = 0; i < tbData.Rows.Count; i++)
            {
                var row = i == 0 ? baseSheet.GetRow(i + 1) : baseSheet.CreateRow(i + 1);
                SetRowData(row, baseSheet, i, tbData, dicData);
            }
        }

        /// <summary>
        /// 复制行
        /// </summary>
        /// <param name="sheet">sheet</param>
        /// <param name="sourceIndex">源数据行</param>
        /// <param name="targetIndex">目标行</param>
        public static void CopyRowRelative(this ISheet sheet, int sourceIndex, int targetIndex)
        {
            GuardUtils.NotNull(sheet, nameof(sheet));
            sheet.CopyRow(sourceIndex, targetIndex);
            var row = sheet.GetRow(targetIndex);
            var columnCount = row.Cells.Count;
            for (int j = 0; j < columnCount; j++)
            {
                var cell = row.GetCell(j);
                if (cell.CellType == CellType.Formula)
                {
                    var formula = cell.CellFormula;
                    formula = CopyFormula(formula, sheet, targetIndex);
                    cell.SetCellFormula(formula);
                }
            }
        }

        private static string CopyFormula(string formula, ISheet sheet, int targetIndex)
        {
            var workbookWrapper = XSSFEvaluationWorkbook.Create((XSSFWorkbook)sheet.Workbook);
            var ptgs = FormulaParser.Parse(formula, workbookWrapper, FormulaType.Cell, sheet.Workbook.GetSheetIndex(sheet));
            foreach (var ptg in ptgs)
            {
                if (ptg is RefPtgBase refs && refs.IsRowRelative)
                {
                    refs.Row = targetIndex;
                }
            }

            return FormulaRenderer.ToFormulaString(workbookWrapper, ptgs);
        }

        private static void SetRowData(IRow row, ISheet baseSheet, int rowIdx, DataTable tbData, Dictionary<string, int> dicData)
        {
            var baseColumnCount = baseSheet.GetRow(0).Cells.Count;
            for (int j = 0; j < baseColumnCount; j++)
            {
                var key = baseSheet.GetRow(0).GetCell(j).StringCellValue;
                if (rowIdx == 0)
                {
                    var val = string.Empty;
                    if (dicData.TryGetValue(key, out int idx))
                    {
                        val = tbData.Rows[rowIdx][idx].ToString();
                    }

                    row.GetCell(j).SetCellValue(val);
                }
                else
                {
                    var cell = row.CreateCell(j);
                    var val = string.Empty;
                    if (dicData.TryGetValue(key, out int idx))
                    {
                        val = tbData.Rows[rowIdx][idx].ToString();
                    }

                    cell.SetCellValue(val);
                }
            }
        }

        private static Dictionary<string, int> GetTitleDic(DataTable tbData, ISheet baseSheet)
        {
            GuardUtils.NotNull(baseSheet, nameof(baseSheet));
            var baseColumnCount = baseSheet.GetRow(0).Cells.Count;
            var dicData = new Dictionary<string, int>();
            for (int j = 0; j < baseColumnCount; j++)
            {
                var expression = baseSheet.GetRow(1).GetCell(j).StringCellValue;
                if (!string.IsNullOrEmpty(expression))
                {
                    SetTitleDic(dicData, expression, tbData);
                }
                else
                {
                    throw new ArgumentException($"模板格式错误");
                }
            }

            return dicData;
        }

        private static void SetTitleDic(Dictionary<string, int> dicData, string expression, DataTable tbData)
        {
            var key = expression.Split('.').Last();
            for (int i = 0; i < tbData.Columns.Count; i++)
            {
                if (key == tbData.Columns[i].ColumnName)
                {
                    dicData.Add(key, i);
                }
            }
        }

        private static List<ExcelImportErroInfo> GetDataTypeError<T>(Dictionary<PropertyInfo, string> needDataTypeValdiate, T info, dynamic originalInfo)
        {
            var errors = new List<ExcelImportErroInfo>();
            needDataTypeValdiate.Each(requie =>
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                properties.Each(x =>
                {
                    var dataTypeAttribute = requie.Key.GetCustomAttribute<DataTypeValidAttribute>();
                    var columnNameAttribute = x.GetCustomAttribute<ColumnNameAttribute>();
                    if (dataTypeAttribute != null && columnNameAttribute != null && dataTypeAttribute.ColumnName == columnNameAttribute.ColumnName)
                    {
                        var val = x.GetValue(originalInfo, null);
                        if (val != null && val.ToString() != string.Empty)
                         {
                            List<ExcelImportErroInfo> error = ConvertDataType<T>(requie, info, originalInfo, val);

                            if (error.Any())
                            {
                                errors.AddRange(error);
                            }
                        }
                    }
                });
            });

            return errors;
        }

        private static List<ExcelImportErroInfo> ConvertDataType<T>(KeyValuePair<PropertyInfo, string> requie, T info, dynamic originalInfo, dynamic val)
        {
            var error = new List<ExcelImportErroInfo>();
            if (requie.Key.PropertyType == typeof(decimal) || requie.Key.PropertyType == typeof(decimal?))
            {
                decimal result;
                if (!decimal.TryParse(val, out result))
                {
                    error.Add(new ExcelImportErroInfo
                    {
                        Row = originalInfo.RowNum,
                        Coloumn = requie.Value,
                        Message = requie.Value + "数据格式错误"
                    });
                }
                else
                {
                    requie.Key.SetValue(info, result, null);
                }
            }

            if (requie.Key.PropertyType == typeof(int) || requie.Key.PropertyType == typeof(int?))
            {
                int result;
                if (!int.TryParse(val, out result))
                {
                    error.Add(new ExcelImportErroInfo
                    {
                        Row = originalInfo.RowNum,
                        Coloumn = requie.Value,
                        Message = requie.Value + "数据格式错误"
                    });
                }
                else
                {
                    requie.Key.SetValue(info, result, null);
                }
            }

            if (requie.Key.PropertyType == typeof(DateTime) || requie.Key.PropertyType == typeof(DateTime?))
            {
                DateTime result;
                if (!DateTime.TryParse(val, out result))
                {
                    error.Add(new ExcelImportErroInfo
                    {
                        Row = originalInfo.RowNum,
                        Coloumn = requie.Value,
                        Message = requie.Value + "时间格式错误"
                    });
                }
                else
                {
                    requie.Key.SetValue(info, result, null);
                }
            }

            return error;
        }

        private static List<ExcelImportErroInfo> GetRequiredError(Dictionary<PropertyInfo, string> requied, dynamic originalInfo)
        {
            var error = new List<ExcelImportErroInfo>();
            requied.Each(requie =>
            {
                var val = requie.Key.GetValue(originalInfo, null);

                if (val == null || val.ToString() == string.Empty)
                {
                    error.Add(new ExcelImportErroInfo { Row = originalInfo.RowNum, Coloumn = requie.Value, Message = requie.Value + "为必填" });
                }
            });

            return error;
        }

        #region 导入导出excel私有方法
        /// <summary>
        /// 通过已选列获取属性集合
        /// </summary>
        /// <param name="props">属性集合</param>
        /// <param name="selectedColumns">已选列</param>
        /// <returns>已选列属性集合</returns>
        private static PropertyInfo[] GetPropertyByColumn(this IEnumerable<PropertyInfo> props, IList<string> selectedColumns)
        {
            var properties = props.Where(x => x.GetCustomAttributes(typeof(ExportExcelAttribute), false).Any());
            if (selectedColumns.Any(x => x != string.Empty))
            {
                properties = properties.Where(x => selectedColumns.Contains(x.Name));
            }

            return properties.ToArray();
        }

        private static Dictionary<PropertyInfo, string> Valdiate<T>()
        {
            var type = typeof(T);

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dic = new Dictionary<PropertyInfo, string>();

            properties.Each((x) =>
            {
                var attributes = x.GetCustomAttributes(typeof(RequiredAttribute), false) as RequiredAttribute[];

                if (attributes != null && attributes.Length > 0 && attributes[0].IsRequired)
                {
                    var colAttributes = x.GetCustomAttributes(typeof(ColumnNameAttribute), false) as ColumnNameAttribute[];
                    var columnName = string.Empty;
                    if (colAttributes != null && colAttributes.Length > 0)
                    {
                        var columnNameAttribute = colAttributes[0];
                        if (columnNameAttribute != null)
                        {
                            columnName = columnNameAttribute.ColumnName;
                        }
                    }

                    dic.Add(x, string.IsNullOrEmpty(columnName) ? x.Name : columnName);
                }
            });

            return dic;
        }

        /// <summary>
        /// 获取Excel的标题行
        /// </summary>
        /// <param name="book">传入的WorkBook</param>
        /// <param name="sheet">Sheet</param>
        /// <param name="dt">DataTable</param>
        /// <param name="exportExcelAttributes">可设置的特性</param>
        /// <param name="isShowSequenced">是否显示序号</param>
        /// <param name="isDynamic">动态列</param>
        private static void SetTitle(HSSFWorkbook book, ISheet sheet, DataTable dt, IList<ExportExcelAttribute> exportExcelAttributes, bool isShowSequenced, bool? isDynamic = false)
        {
            IRow row = sheet.CreateRow(0);

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICellStyle topStyle = book.CreateCellStyle();
                topStyle.Alignment = HorizontalAlignment.Center;
                IFont font = book.CreateFont();
                font.FontHeightInPoints = FontHeightInPoints;
                font.Boldweight = (short)FontBoldWeight.Bold;
                topStyle.SetFont(font);
                var cell = row.CreateCell(j);
                cell.CellStyle = topStyle;
                cell.SetCellValue(dt.Columns[j].ColumnName);

                if (isShowSequenced)
                {
                    if (j == 0)
                    {
                        sheet.SetColumnWidth(j, ExcelClounmConst.ExclNumberSize);
                    }
                    else
                    {
                        SetColumnWidth(sheet, j, j - 1, exportExcelAttributes, isDynamic.GetValueOrDefault());
                    }
                }
                else
                {
                    if (j != dt.Columns.Count - 1)
                    {
                        SetColumnWidth(sheet, j, j, exportExcelAttributes, isDynamic.GetValueOrDefault());
                    }
                }
            }

            sheet.CreateFreezePane(0, 1, 0, 1);
        }

        private static void SetColumnWidth(ISheet sheet, int colIdx, int attributeIdx, IList<ExportExcelAttribute> exportExcelAttributes, bool isDynamic)
        {
            if (isDynamic)
            {
                sheet.AutoSizeColumn(colIdx);
            }
            else
            {
                sheet.SetColumnWidth(colIdx, exportExcelAttributes[attributeIdx].ColunmSize);
            }
        }

        /// <summary>
        /// 构建每个Sheet数据行
        /// </summary>
        /// <param name="book">Workbook</param>
        /// <param name="sheet">Sheet</param>
        /// <param name="dt">DataTable</param>
        /// <param name="isShowSequenced">是否显示序号</param>
        private static void SetDataRow(HSSFWorkbook book, ISheet sheet, DataTable dt, bool isShowSequenced)
        {
            ICellStyle cellStyle = book.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Center;
            IFont font = book.CreateFont();
            font.FontHeightInPoints = FontHeightInPoints;
            font.Color = HSSFColor.LightBlue.Index;
            cellStyle.SetFont(font);

            ICellStyle cellStyleDouble = book.CreateCellStyle();
            cellStyleDouble.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");

            ICellStyle cellStyleStr = book.CreateCellStyle();
            cellStyleStr.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");

            ICellStyle cellStylePercent = book.CreateCellStyle();
            cellStylePercent.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    var cell = row.CreateCell(j);
                    var value = dt.Rows[i][j].ToString();
                    SetCellValue(cell, value, dt.Columns[j].DataType, cellStyleDouble, cellStyleStr);

                    if (j == 0 && isShowSequenced)
                    {
                        cell.CellStyle = cellStyle;
                    }
                }
            }
        }

        private static void SetCellValue(ICell cell, string value, Type type, ICellStyle cellStyleDouble, ICellStyle cellStyleStr)
        {
            if (type == typeof(decimal) || type == typeof(double))
            {
                decimal num;
                if (decimal.TryParse(value, out num))
                {
                    cell.SetCellValue(Convert.ToDouble(num));
                    cell.CellStyle = cellStyleDouble;
                }
                else
                {
                    cell.CellStyle = cellStyleStr;
                    cell.SetCellValue(value);
                }
            }
            else if (type == typeof(int))
            {
                int num;

                if (int.TryParse(value, out num))
                {
                    cell.SetCellValue(num);
                }
            }
            else
            {
                cell.CellStyle = cellStyleStr;
                cell.SetCellValue(value);
            }
        }

        /// <summary>
        /// 给DataTable绑定列
        /// </summary>
        /// <param name="tb">DataTable</param>
        /// <param name="props">属性集合</param>
        /// <param name="isShowSequenced">是否显示序号</param>
        private static void SetDataTableColunm(DataTable tb, IEnumerable<PropertyInfo> props, bool isShowSequenced)
        {
            if (isShowSequenced)
            {
                tb.Columns.Add("序号");
            }

            foreach (PropertyInfo prop in props)
            {
                var customAttributes = prop.GetCustomAttributes(typeof(ExportExcelAttribute), false);
                if (customAttributes.Any())
                {
                    foreach (var attr in customAttributes.Where(x => x is ExportExcelAttribute))
                    {
                        var exportAttr = attr as ExportExcelAttribute;
                        tb.Columns.Add(exportAttr.ColunmName, prop.PropertyType.GetCoreType());
                    }
                }
            }
        }

        /// <summary>
        /// 获取DataTable所绑定列的值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tb">DataTable</param>
        /// <param name="props">泛型所具有的属性</param>
        /// <param name="items">泛型集合</param>
        /// <param name="isShowSequenced">是否显示序号</param>
        private static void SetDataTableColunmValue<T>(DataTable tb, IEnumerable<PropertyInfo> props, IEnumerable<T> items, bool isShowSequenced)
        {
            var properties = props as PropertyInfo[] ?? props.ToArray();
            var length = properties.Count();

            if (isShowSequenced)
            {
                ////+1是为了增加序号的列
                length += 1;
            }

            var values = new object[length];

            int rowIndex = 0;
            foreach (T item in items)
            {
                rowIndex++;
                for (int i = 0; i < length; i++)
                {
                    if (isShowSequenced)
                    {
                        values[i] = i == 0 ? rowIndex : properties[i - 1].GetValue(item, null);
                    }
                    else
                    {
                        values[i] = properties[i].GetValue(item, null);
                    }
                }

                tb.Rows.Add(values);
            }
        }

        /// <summary>
        /// 把集合转换为DataTable
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="items">数据</param>
        /// <param name="selectedColumns">选中列</param>
        /// <param name="isShowSequenced">是否序号</param>
        /// <returns>DataTable</returns>
        private static DataTable[] ToDataTable<T>(IEnumerable<T> items, IList<string> selectedColumns, bool isShowSequenced)
        {
            var tbs = new List<DataTable>();
            var enumerable = items as T[] ?? items.ToArray();
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).GetPropertyByColumn(selectedColumns);
            for (var i = 0; i < (enumerable.Count() / RowPageSize) + 1; i++)
            {
                var newItems = enumerable.Skip(i * RowPageSize).Take(RowPageSize).ToList();
                var tb = new DataTable(typeof(T).Name);

                ////设置Datable列                
                SetDataTableColunm(tb, props, isShowSequenced);

                ////设置DataTable列值
                SetDataTableColunmValue(tb, props, newItems, isShowSequenced);

                tbs.Add(tb);
            }

            return tbs.ToArray();
        }

        /// <summary>
        /// 获取到字段的特性
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="selectedColumns">可选列</param>
        /// <returns>ExportExcelAttribute集合</returns>
        private static List<ExportExcelAttribute> GetAttributesByClassName<T>(IList<string> selectedColumns)
        {
            var exportExcelAttributes = new List<ExportExcelAttribute>();
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).GetPropertyByColumn(selectedColumns);
            foreach (PropertyInfo prop in props)
            {
                var descAttr = prop.GetCustomAttributes(typeof(ExportExcelAttribute), false);

                if (descAttr.Any())
                {
                    exportExcelAttributes.AddRange(descAttr.OfType<ExportExcelAttribute>());
                }
            }

            return exportExcelAttributes;
        }

        /// <summary>
        /// 行数据转换为对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="row">行</param>
        /// <param name="rel">属性</param>
        /// <returns>对象</returns>
        private static T Row2Object<T>(IRow row, Dictionary<int, PropertyInfo> rel) where T : class, new()
        {
            var t = new T();

            foreach (var propertyInfo in rel)
            {
                var cell = row.GetCell(propertyInfo.Key);
                if (cell != null)
                {
                    PropertyInfo p = propertyInfo.Value;
                    p.SetValue(t, cell.ToString(), null);
                }
            }

            return t;
        }

        /// <summary>
        /// 生成ExcelWorkBookInfo
        /// </summary>
        /// <param name="dts">数据dataTable</param>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="fileName">文件名</param>
        /// <param name="choiseStr">选择列的字符串</param>
        /// <returns>ExcelWorkBookInfo对象</returns>
        private static ExcelWorkBookInfo ConverteExcelWorkBook(List<DataTable> dts, string sheetName, string fileName, string choiseStr)
        {
            var sheetNames = new List<string>();
            var choiseStrs = new List<string>();
            for (int i = 0; i < dts.Count; i++)
            {
                if (i > 0)
                {
                    sheetNames.Add(sheetName + (i + 1));
                }
                else
                {
                    sheetNames.Add(sheetName);
                }

                choiseStrs.Add(choiseStr);
            }

            var book = new ExcelWorkBookInfo()
            {
                FileName = fileName,
                SheetName = sheetNames.ToArray(),
                Data = dts,
                ChoiseStr = choiseStrs.ToArray()
            };

            return book;
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>属性-ColumnName</returns>
        private static Dictionary<PropertyInfo, string> DataTypeValdiate<T>()
        {
            var type = typeof(T);

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dic = new Dictionary<PropertyInfo, string>();

            properties.Each(x =>
            {
                var attributes = x.GetCustomAttribute<DataTypeValidAttribute>();
                if (attributes != null)
                {
                    dic.Add(x, string.IsNullOrEmpty(attributes.ColumnName) ? x.Name : attributes.ColumnName);
                }
            });

            return dic;
        }
        #endregion
    }
}
