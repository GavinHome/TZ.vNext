'use strict';

console.log();
process.on('exit', () => {
  console.log();
});

if (!process.argv[2]) {
  console.error('[组件名]必填 - Please enter new component name');
  process.exit(1);
}

var dateFormat = require('dateformat');
const path = require('path');
const fileSave = require('file-save');
const uppercamelcase = require('uppercamelcase');
const componentname = process.argv[2];
const chineseName = process.argv[3] || componentname;
const ComponentName = uppercamelcase(componentname);
// const PackagePath = path.resolve(__dirname, '../../packages', componentname);
const Files = [
  {
    filename: `${ComponentName}.cs`,
    path: '../TZIWB.vNext.Model/Model',
    content: `//-----------------------------------------------------------------------------------
    // <copyright file="${ComponentName}.cs" company="天职工程咨询股份有限公司版权所有">
    //     Copyright  TZEPM. All rights reserved.
    // </copyright>
    // <author>tzxx</author>
    // <date>${dateFormat(new Date(), 'yyyy-m-d H:MM:ss')}</date>
    // <description></description>
    //-----------------------------------------------------------------------------------
    
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using TZIWB.vNext.Core.Entity;
    using TZIWB.vNext.Core.Enum;
    using TZIWB.vNext.Model.Enum;
    
    namespace TZIWB.vNext.Model
    {
        /// <summary>
        /// ${chineseName}
        /// </summary>
        [Table("T_${ComponentName}")]
        public class ${ComponentName} : EntitySetWithCreate
        {
        }

        public string Property1 { get; set; }

        public string Property2 { get; set; }
    }
    `
  },
  {
    filename: `${ComponentName}Info.cs`,
    path: '../TZIWB.vNext.ViewModel',
    content: `//-----------------------------------------------------------------------------------
    // <copyright file="${ComponentName}.cs" company="天职工程咨询股份有限公司版权所有">
    //     Copyright  TZEPM. All rights reserved.
    // </copyright>
    // <author>tzxx</author>
    // <date>${dateFormat(new Date(), 'yyyy-m-d H:MM:ss')}</date>
    // <description></description>
    //-----------------------------------------------------------------------------------
    
    using System;
    using System.Collections.Generic;
    using TZIWB.vNext.Core.Enum;
    using TZIWB.vNext.Core.Extensions;
    
    namespace TZIWB.vNext.ViewModel
    {
        /// <summary>
        /// ${chineseName}
        /// </summary>
        public class ${ComponentName}Info : BaseInfo
        {
        }

        public string Property1 { get; set; }

        public string Property2 { get; set; }
    }
    `
  },
  {
    filename: `I${ComponentName}Db.cs`,
    path: '../TZIWB.vNext.Database.Contracts',
    content: `//-----------------------------------------------------------------------------------
    // <copyright file="I${ComponentName}Db.cs" company="天职工程咨询股份有限公司版权所有">
    //     Copyright  TZEPM. All rights reserved.
    // </copyright>
    // <author>tzxx</author>
    // <date>${dateFormat(new Date(), 'yyyy-m-d H:MM:ss')}</date>
    // <description></description>
    //-----------------------------------------------------------------------------------
    
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TZIWB.vNext.Model;
    
    namespace TZIWB.vNext.Database.Contracts
    {
        /// <summary>
        /// ${chineseName}
        /// </summary>
        public interface I${ComponentName}Db : IDbCommon
        {
        } 
    }
    `
  },
  {
    filename: `${ComponentName}Db.cs`,
    path: '../TZIWB.vNext.DataBase.Implement',
    content: `//-----------------------------------------------------------------------------------
    // <copyright file="${ComponentName}Db.cs" company="天职工程咨询股份有限公司版权所有">
    //     Copyright  TZEPM. All rights reserved.
    // </copyright>
    // <author>tzxx</author>
    // <date>${dateFormat(new Date(), 'yyyy-m-d H:MM:ss')}</date>
    // <description></description>
    //-----------------------------------------------------------------------------------
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TZIWB.vNext.Database.Contracts;
    using TZIWB.vNext.Model;
    using TZIWB.vNext.Model.Context;
    using TZIWB.vNext.ViewModel;
    
    namespace TZIWB.vNext.DataBase.Implement
    {
        /// <summary>
        /// ${chineseName}
        /// </summary>
        public class ${ComponentName}Db : DbCommon, I${ComponentName}Db
        {
            public ${ComponentName}Db(AppDbContext dbcontext) : base(dbcontext)
            {
            }
        }
    }
    `
  },
  {
    filename: `I${ComponentName}Service.cs`,
    path: '../TZIWB.vNext.Service.Contracts',
    content: `//-----------------------------------------------------------------------------------
    // <copyright file="I${ComponentName}Service.cs" company="天职工程咨询股份有限公司版权所有">
    //     Copyright  TZEPM. All rights reserved.
    // </copyright>
    // <author>tzxx</author>
    // <date>${dateFormat(new Date(), 'yyyy-m-d H:MM:ss')}</date>
    // <description></description>
    //-----------------------------------------------------------------------------------
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TZIWB.vNext.Model;
    using TZIWB.vNext.ViewModel;
    
    namespace TZIWB.vNext.Services.Contracts
    {
        /// <summary>
        /// ${chineseName}
        /// </summary>
        public interface I${ComponentName}Service
        {
        }
    }
    `
  },
  {
    filename: `${ComponentName}Service.cs`,
    path: '../TZIWB.vNext.Service.Implement',
    content: `//-----------------------------------------------------------------------------------
    // <copyright file="${ComponentName}Service.cs" company="天职工程咨询股份有限公司版权所有">
    //     Copyright  TZEPM. All rights reserved.
    // </copyright>
    // <author>tzxx</author>
    // <date>${dateFormat(new Date(), 'yyyy-m-d H:MM:ss')}</date>
    // <description></description>
    //-----------------------------------------------------------------------------------
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TZIWB.vNext.Core.Enum;
    using TZIWB.vNext.Database.Contracts;
    using TZIWB.vNext.Model;
    using TZIWB.vNext.Services.Contracts.Salary;
    using TZIWB.vNext.ViewModel;
    
    namespace TZIWB.vNext.Services.Implement
    {
        /// <summary>
        /// ${chineseName}
        /// </summary>
        public class ${ComponentName}Service : I${ComponentName}Service
        {
        }
    }`
  },
  {
    filename: `${ComponentName}Controller.cs`,
    path: './Controllers',
    content: `//-----------------------------------------------------------------------------------
    // <copyright file="${ComponentName}Controller.cs" company="天职工程咨询股份有限公司版权所有">
    //     Copyright  TZEPM. All rights reserved.
    // </copyright>
    // <author>tzxx</author>
    // <date>${dateFormat(new Date(), 'yyyy-m-d H:MM:ss')}</date>
    // <description></description>
    //-----------------------------------------------------------------------------------
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Kendo.Mvc.UI;
    using MimeKit;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;
    using NPOI.XSSF.UserModel;
    using TZIWB.vNext.Core.Cache;
    using TZIWB.vNext.Core.Enum;
    using TZIWB.vNext.Services.Contracts;
    using TZIWB.vNext.Services.Contracts.Salary;
    using TZIWB.vNext.ViewModel;
    using TZIWB.vNext.Web.Extensions.KendoUiExtensions;
    using TZIWB.vNext.ViewModel.Schema;
    using TZIWB.vNext.Core.Const;
    using TZIWB.vNext.Web.PermissionExtensions;
    using TZIWB.vNext.Core.Utility;
    
    namespace TZIWB.vNext.Web.Controllers
    {
        [Produces("application/json")]
        [Route("api/[controller]")]
        public class ${ComponentName}Controller : AuthorizeController
        {
            private readonly I${ComponentName}Service _${ComponentName}Service;
    
            public SalaryTemplatesController(I${ComponentName}Service ${ComponentName}Service)
            {
                _${ComponentName}Service = ${ComponentName}Service;
            }
        }
    }
    `
  }, 
  {
    filename: `${ComponentName}.grid.vue.html`,
    path: './ClientApp/components/model',
    content: `export class ${ComponentName} {
                  Id?: string;
                  Property1?: string;
                  Property2?: string;
              
                  constructor() {
                  }
              }`
  }, 
  {
    filename: `${ComponentName}.grid.vue.html`,
    path: './ClientApp/components/pages',
    content: `<template>
                <div>
                    <tz-grid-dynamic :transport_read_url="readUrl" :columns="columns" ref="${ComponentName}Grid">
                        <template slot-scope="{ dataSource }">
                            <el-button type="primary" size="small" :round="false" @click="create">新增${chineseName}</el-button>
                        </template>
                    </tz-grid-dynamic>
                </div>
            </template>

            <script src="./${ComponentName}.grid.ts"></script>`
  },
  {
    filename: `${ComponentName}.grid.ts`,
    path: './ClientApp/components/pages',
    content: `import Vue from 'vue';
    import { Component } from 'vue-property-decorator'
    
    import 'element-ui/lib/theme-chalk/index.css'
    import { Button, Message } from 'element-ui'
    import { GridMenuType, EnumHelper, FieldTypeEnum, EnumConstType } from '../../common/Enums'
    import kendoExtensions from '../../extension/KendoExtensions'
    import { GridCommon } from '../../extension/GridExtensions'
    import { GridColumnSchema } from '../../schemas/GridColumnSchema'
    import { TzConfirm } from '../../common/TzDialog';
    import { TzApiConst, TzMessageConst } from '../../common/TzCommonConst';
    
    Vue.use(Button)
    
    @Component({
        components: {
            TzGridDynamic: require('../../wrapper/TzGridDynamic.vue.html')
        }
    })
    export default class ${ComponentName}GridComponent extends Vue {
        columnsData: GridColumnSchema[] = [
            {
                field: "RowNumber",
                title: "序号",
                width: "8%",
                filterable: false,
                sortable: false,
                editable: false,
                menu: false,
                type: FieldTypeEnum.Number,
                index: 0
            },
            {
                field: "Property1",
                title: "字段1",
                filterable: true,
                sortable: true,
                editable: false,
                menu: true,
                width: "22%",
                type: FieldTypeEnum.String,
                index: 1
            },
            {
                field: "Property2",
                title: "字段2",
                filterable: true,
                sortable: true,
                editable: true,
                menu: true,
                width: "40%",
                index: 3,
                type: FieldTypeEnum.String,
                hidden: false
            },
            {
                title: "",
                filterable: false,
                sortable: false,
                editable: false,
                menu: false,
                command: this.commands,
                width: "15%",
                type: FieldTypeEnum.Command,
                index: 99
            }
        ];
    
        get readUrl() {
            return TzApiConst.${ComponentName}_GRID_QUERY
        }
    
        get columns() {
            return this.columnsData.sort((x, y) => x.index - y.index)
        }
    
        get commands() {
            var commands = [
                {
                    name: GridMenuType.Detail,
                    title: EnumHelper.toGridMenuTypeString(GridMenuType.Detail),
                    action: this.detail,
                    visible: function (dataItem) {
                        return dataItem.Menus && dataItem.Menus.indexOf("Detail") > -1;
                    },
                    is: true,
                    index: 0,
                    param: {}
                },
                {
                    name: GridMenuType.Edit,
                    title: EnumHelper.toGridMenuTypeString(GridMenuType.Edit),
                    action: this.edit,
                    visible: function (dataItem) {
                        return dataItem.Menus && dataItem.Menus.indexOf("Edit") > -1
                    },
                    is: true,
                    index: 1,
                    param: {}
                },
                {
                    name: GridMenuType.Enable,
                    title: EnumHelper.toGridMenuTypeString(GridMenuType.Enable),
                    action: this.enable,
                    visible: function (dataItem) {
                        return dataItem.Menus && dataItem.Menus.indexOf("Enable") > -1
                    },
                    is: true,
                    index: 2,
                    param: {}
                },
                {
                    name: GridMenuType.Disable,
                    title: EnumHelper.toGridMenuTypeString(GridMenuType.Disable),
                    action: this.disable,
                    visible: function (dataItem) {
                        return dataItem.Menus && dataItem.Menus.indexOf("Disable") > -1
                    },
                    is: true,
                    index: 2,
                    param: {}
                }
            ];
    
            var commandsTemplate = commands.filter(x => x.is).sort(x => x.index)
            return new GridCommon().bindCommands(commandsTemplate)
        }
    
        create() {
          //TODO: write your code in here
        }

        detail(e) {
          var data = kendoExtensions.getRowData(e)
          //TODO: write your code in here
        }
    
        edit(e) {
            var data = kendoExtensions.getRowData(e)
            //TODO: write your code in here
        }
    
        enable(e) {
            var data = { id: kendoExtensions.getRowData(e).Id }
            //TODO: write your code in here
        }
    
        disable(e) {
            var data = { id: kendoExtensions.getRowData(e).Id }
            //TODO: write your code in here
        }
    }`
  },  
  {
    filename: `${ComponentName}.create.vue.html`,
    path: './ClientApp/components/pages',
    content: `<template>
                <el-form label-position="top" label-width="80px" :model="model" :rules="rules" ref="ruleForm">
                    <el-collapse :value="['baseInfo']">
                        <el-collapse-item title="基本信息" name="baseInfo">
                            <el-row :gutter="24">
                                <el-col :span="24">
                                    <el-form-item label="字段1：" size="medium" prop="Property1">
                                      <el-input v-model="model.Property1"></el-input>
                                    </el-form-item>
                                </el-col>
                            </el-row>
                            <el-row :gutter="24">
                                <el-col :span="24">
                                    <el-form-item label="字段2：" size="medium" prop="Property2">
                                      <el-input v-model="model.Property2"></el-input>
                                    </el-form-item>
                                </el-col>
                            </el-row>
                        </el-collapse-item>
                    </el-collapse>
                    <el-row class="mb15 mt15">
                        <el-form-item class="text-right">
                            <el-button type="primary" @click="submitForm('ruleForm')">保存</el-button>
                            <el-button @click="$router.go(-1)">返回</el-button>
                        </el-form-item>
                    </el-row>
                </el-form>
            </template>

            <script src="./${ComponentName}.create.ts"></script>`
  },
  {
    filename: `${ComponentName}.create.ts`,
    path: './ClientApp/components/pages',
    content: `import Vue from "vue";
    import { Component, Prop } from "vue-property-decorator";
    import { Form, FormItem, Input, Row, Col, Collapse, CollapseItem, Message } from "element-ui"
    import "element-ui/lib/theme-chalk/index.css";
    import { ${Componentname} } from "../../model/${Componentname}";
    import remote, { filesValidator } from "../../common/TzValidators";
    import { ISelect } from "../../schemas/SelectItemSchema";
    import { TzApiConst, TzMessageConst, TzRuleMsgConst } from "../../common/TzCommonConst";
    import { TzFetch } from "../../common/TzFetch";
    
    Vue.use(Form)
    Vue.use(FormItem)
    Vue.use(Input)
    Vue.use(Row)
    Vue.use(Col)
    Vue.use(Collapse)
    Vue.use(CollapseItem)
    
    @Component({
        props: ["id"],
        components: {
        }
    })
    
    export default class ${ComponentName}CreateComponent extends Vue {
        @Prop() id!: string;
        model = new ${Componentname}()
        
        get rules() {
            return {
                Property1: [{ required: true, message: "", trigger: "change" }],
                Property2: [{ required: true, message: "", trigger: "change" }]
            }
        }
    
        created() {
            if (this.id) {
              //TODO: write your code in here
            }
        }
    
        mounted() {
          //TODO: write your code in here
        }
    
        submitForm(formName) {
          //TODO: write your code in here
        }
    }`
  },  
  {
    filename: `${ComponentName}.detail.vue.html`,
    path: './ClientApp/components/pages',
    content: `<template>
                <el-form label-position="top" label-width="80px" :model="model">
                    <el-collapse :value="['baseInfo']">
                        <el-collapse-item title="基本信息" name="baseInfo">
                            <el-row :gutter="24">
                                <el-col :span="24">
                                    <el-form-item label="字段1：" size="medium" class="readonly">
                                      <el-input v-model="model.Property1" :readonly="true"></el-input>
                                    </el-form-item>
                                </el-col>
                            </el-row>
                            <el-row :gutter="24">
                                <el-col :span="24">
                                    <el-form-item label="字段2：" size="medium" prop="Property2" class="readonly">
                                      <el-input v-model="model.Property2" :readonly="true"></el-input>
                                    </el-form-item>
                                </el-col>
                            </el-row>
                        </el-collapse-item>
                    </el-collapse>
                    <el-row class="mb15 mt15">
                        <el-form-item class="text-right">
                            <el-button @click="$router.go(-1)">返回</el-button>
                        </el-form-item>
                    </el-row>
                </el-form>
            </template>

            <script src="./${ComponentName}.detail.ts"></script>`
  },
  {
    filename: `${ComponentName}.detail.ts`,
    path: './ClientApp/components/pages',
    content: `import Vue from "vue"
              import { Component, Prop } from "vue-property-decorator"

              import "element-ui/lib/theme-chalk/index.css"
              import { Form, FormItem, Input, Row, Col, Button, Collapse, CollapseItem, Message } from "element-ui"

              import { ${ComponentName} } from "../../model/${ComponentName}"
              import { TzApiConst, TzMessageConst } from "../../common/TzCommonConst";
              import { TzFetch } from "../../common/TzFetch";

              Vue.use(Form)
              Vue.use(FormItem)
              Vue.use(Input)
              Vue.use(Row)
              Vue.use(Col)
              Vue.use(Button)
              Vue.use(Collapse)
              Vue.use(CollapseItem)

              @Component({
                  props: ["id"]
              })
              export default class ${ComponentName}DetailComponent extends Vue {
                  @Prop() id!: string
                  model = new ${ComponentName}()

                  created() {
                      if (this.id) {
                        //TODO: write your code in here
                      }
                  }
              }`
  }
];

// 创建
Files.forEach(file => {
  fileSave(path.join(file.path, file.filename))
    .write(file.content, 'utf8')
    .end('\n');
});

// const Files = [
//   {
//     filename: 'index.js',
//     content: `import ${ComponentName} from './src/main';

// /* istanbul ignore next */
// ${ComponentName}.install = function(Vue) {
//   Vue.component(${ComponentName}.name, ${ComponentName});
// };

// export default ${ComponentName};`
//   },
//   {
//     filename: 'src/main.vue',
//     content: `<template>
//   <div class="el-${componentname}"></div>
// </template>

// <script>
// export default {
//   name: 'El${ComponentName}'
// };
// </script>`
//   },
//   {
//     filename: path.join('../../examples/docs/zh-CN', `${componentname}.md`),
//     content: `## ${ComponentName} ${chineseName}`
//   },
//   {
//     filename: path.join('../../examples/docs/en-US', `${componentname}.md`),
//     content: `## ${ComponentName}`
//   },
//   {
//     filename: path.join('../../examples/docs/es', `${componentname}.md`),
//     content: `## ${ComponentName}`
//   },
//   {
//     filename: path.join('../../test/unit/specs', `${componentname}.spec.js`),
//     content: `import { createTest, destroyVM } from '../util';
// import ${ComponentName} from 'packages/${componentname}';

// describe('${ComponentName}', () => {
//   let vm;
//   afterEach(() => {
//     destroyVM(vm);
//   });

//   it('create', () => {
//     vm = createTest(${ComponentName}, true);
//     expect(vm.$el).to.exist;
//   });
// });
// `
//   },
//   {
//     filename: path.join('../../packages/theme-chalk/src', `${componentname}.scss`),
//     content: `@import "mixins/mixins";
// @import "common/var";

// @include b(${componentname}) {
// }`
//   },
//   {
//     filename: path.join('../../types', `${componentname}.d.ts`),
//     content: `import { ElementUIComponent } from './component'

// /** ${ComponentName} Component */
// export declare class El${ComponentName} extends ElementUIComponent {
// }`
//   }
// ];

// // 添加到 components.json
// const componentsFile = require('../../components.json');
// if (componentsFile[componentname]) {
//   console.error(`${componentname} 已存在.`);
//   process.exit(1);
// }
// componentsFile[componentname] = `./packages/${componentname}/index.js`;
// fileSave(path.join(__dirname, '../../components.json'))
//   .write(JSON.stringify(componentsFile, null, '  '), 'utf8')
//   .end('\n');

// // 创建 package
// Files.forEach(file => {
//   fileSave(path.join(PackagePath, file.filename))
//     .write(file.content, 'utf8')
//     .end('\n');
// });

// // 添加到 nav.config.json
// const navConfigFile = require('../../examples/nav.config.json');

// Object.keys(navConfigFile).forEach(lang => {
//   let groups = navConfigFile[lang][4].groups;
//   groups[groups.length - 1].list.push({
//     path: `/${componentname}`,
//     title: lang === 'zh-CN' && componentname !== chineseName
//       ? `${ComponentName} ${chineseName}`
//       : ComponentName
//   });
// });

// fileSave(path.join(__dirname, '../../examples/nav.config.json'))
//   .write(JSON.stringify(navConfigFile, null, '  '), 'utf8')
//   .end('\n');

console.log('DONE!');
