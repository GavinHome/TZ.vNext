<?xml version="1.0" encoding="UTF-8"?>
<!--Designed and generated by Altova StyleVision Enterprise Edition 2008 rel. 2 sp2 - see http://www.altova.com/stylevision for more information.-->
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fn="http://www.w3.org/2005/xpath-functions" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata" xmlns:msprop="urn:schemas-microsoft-com:xml-msprop" xmlns:mstns="http://stylecopcmd.sourceforge.net/StyleCopReport.xsd" xmlns:n1="urn:schemas-microsoft-com:xml-msdatasource" xmlns:xdt="http://www.w3.org/2005/xpath-datatypes" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:altova="http://www.altova.com" xpath-default-namespace="mstns">
    <xsl:output version="4.0" method="html" indent="no" encoding="UTF-8" doctype-public="-//W3C//DTD HTML 4.0 Transitional//EN" doctype-system="http://www.w3.org/TR/html4/loose.dtd" />
    <xsl:param name="SV_OutputFormat" select="'HTML'" />
    <xsl:variable name="XML" select="/" />
    <xsl:import-schema schema-location="http://stylecopcmd.sourceforge.net/StyleCopReport.xsd" namespace="http://stylecopcmd.sourceforge.net/StyleCopReport.xsd" />
    <xsl:template match="/">
        <html>
            <head>
                <title />
            </head>
            <body>
                <xsl:for-each select="$XML">
                    <h3 style="color:navy; font-family:verdana; ">
                        <span>
                            <xsl:text>Style Cop Report</xsl:text>
                        </span>
                    </h3>
                    <span style="font-family:Verdana ! important; font-size:12px; font-weight:bold; ">
                        <xsl:text>Total Solutions</xsl:text>
                    </span>
                    <span style="font-family:Verdana ! important; font-size:12px; ">
                        <xsl:text>: </xsl:text>
                    </span>
                    <span style="color:#0080ff; font-family:Verdana ! important; font-size:12px; ">
                        <xsl:value-of select="count(  mstns:StyleCopReport/mstns:Solutions  )" />
                    </span>
                    <br />
                    <span style="font-family:Verdana ! important; font-size:12px; font-weight:bold; ">
                        <xsl:text>Total Source Files</xsl:text>
                    </span>
                    <span style="font-family:Verdana ! important; font-size:12px; ">
                        <xsl:text>: </xsl:text>
                    </span>
                    <span style="color:#0080ff; font-family:Verdana ! important; font-size:12px; ">
                        <xsl:value-of select="count(  //mstns:SourceCodeFiles  )" />
                    </span>
                    <br />
                    <span style="font-family:Verdana ! important; font-size:12px; font-weight:bold; ">
                        <xsl:text>Total Number of Violations</xsl:text>
                    </span>
                    <span style="font-family:Verdana ! important; font-size:12px; ">
                        <xsl:text>: </xsl:text>
                    </span>
                    <span style="color:#0080ff; font-family:Verdana ! important; font-size:12px; ">
                        <xsl:value-of select="count(  //mstns:Violations  )" />
                    </span>
                    <br />
                    <br />
                    <table style="font-family:verdana; font-size:12px; width:100%; " border="0" cellpadding="3" width="100%">
                        <tbody>
                            <tr>
                                <td style="width:80px; ">
                                    <span>
                                        <xsl:text>Violations</xsl:text>
                                    </span>
                                </td>
                                <td style="width:250px; ">
                                    <span>
                                        <xsl:text>Solution Name</xsl:text>
                                    </span>
                                </td>
                                <td>
                                    <span>
                                        <xsl:text>Path</xsl:text>
                                    </span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <xsl:for-each select="mstns:StyleCopReport">
                        <xsl:for-each select="mstns:Solutions">
                            <table style="cursor:pointer; font-family:verdana; font-size:12px; width:100%; " border="0" cellpadding="3" width="100%">
                                <tbody onclick="{concat( concat( &quot;var el = document.getElementById(&apos;solution&quot; ,   mstns:ID  ), &quot;&apos;); if ( el.style.display==&apos;block&apos; ) el.style.display=&apos;none&apos;; else el.style.display=&apos;block&apos;;&quot; )}">
                                    <tr style="background-color:#f0f0f0; ">
                                        <td style="width:80px; ">
                                            <span style="color:#0080ff; font-family:Verdana ! important; ">
                                                <xsl:value-of select="count(  mstns:Projects/mstns:SourceCodeFiles/mstns:Violations  )" />
                                            </span>
                                        </td>
                                        <td style="width:250px; ">
                                            <xsl:for-each select="mstns:Name">
                                                <xsl:apply-templates />
                                            </xsl:for-each>
                                        </td>
                                        <td>
                                            <xsl:for-each select="mstns:Location">
                                                <xsl:apply-templates />
                                            </xsl:for-each>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div style="display:none; margin-left:10px; " id="{concat( &quot;solution&quot;, mstns:ID )}">
                                <xsl:for-each select="mstns:Projects">
                                    <table style="font-family:verdana; font-size:12px; width:100%; " border="0" cellpadding="3" width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="width:80px; ">
                                                    <span>
                                                        <xsl:text>Violations</xsl:text>
                                                    </span>
                                                </td>
                                                <td style="width:250px; ">
                                                    <span>
                                                        <xsl:text>Project Name</xsl:text>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span>
                                                        <xsl:text>Path</xsl:text>
                                                    </span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style="cursor:pointer; font-family:verdana; font-size:12px; width:100%; " border="0" cellpadding="3" width="100%">
                                        <tbody onclick="{concat( concat( &quot;var el = document.getElementById(&apos;project&quot; ,   mstns:ID  ), &quot;&apos;); if ( el.style.display==&apos;block&apos; ) el.style.display=&apos;none&apos;; else el.style.display=&apos;block&apos;;&quot; )}">
                                            <tr style="background-color:#e1e1e1; ">
                                                <td style="width:80px; " height="29">
                                                    <span style="color:#0080ff; font-family:Verdana ! important; ">
                                                        <xsl:value-of select="count(  mstns:SourceCodeFiles/mstns:Violations  )" />
                                                    </span>
                                                </td>
                                                <td style="width:250px; " height="29">
                                                    <xsl:for-each select="mstns:Name">
                                                        <xsl:apply-templates />
                                                    </xsl:for-each>
                                                </td>
                                                <td height="29">
                                                    <xsl:for-each select="mstns:Location">
                                                        <xsl:apply-templates />
                                                    </xsl:for-each>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div style="display:none; margin-left:10px; " id="{concat( &quot;project&quot;, mstns:ID )}">
                                        <table style="font-family:verdana; font-size:12px; width:100%; " border="0" cellpadding="3">
                                            <tbody>
                                                <tr>
                                                    <td style="text-align:left; width:80px; " height="20">
                                                        <span>
                                                            <xsl:text>Violations</xsl:text>
                                                        </span>
                                                    </td>
                                                    <td style="text-align:left; width:250px; " height="20">
                                                        <span>
                                                            <xsl:text>Name</xsl:text>
                                                        </span>
                                                    </td>
                                                    <td style="min-width:500px; text-align:left; " height="20">
                                                        <span>
                                                            <xsl:text>Path</xsl:text>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <xsl:for-each select="mstns:SourceCodeFiles">
                                            <table style="cursor:pointer; font-family:verdana; font-size:12px; width:100%; " border="0" cellpadding="3">
                                                <tbody onclick="{concat( concat( &quot;var el = document.getElementById(&apos;files&quot; ,   mstns:ID  ), &quot;&apos;); if ( el.style.display==&apos;block&apos; ) el.style.display=&apos;none&apos;; else el.style.display=&apos;block&apos;;&quot; )}">
                                                    <tr style="background-color:#d2d2d2; ">
                                                        <td style="text-align:left; width:80px; ">
                                                            <span style="color:#0080ff; font-family:Verdana ! important; ">
                                                                <xsl:value-of select="count( mstns:Violations )" />
                                                            </span>
                                                        </td>
                                                        <td style="text-align:left; width:250px; ">
                                                            <xsl:for-each select="mstns:Name">
                                                                <span style="cursor:pointer; font-family:Verdana ! important; text-decoration:none; ">
                                                                    <xsl:apply-templates />
                                                                </span>
                                                            </xsl:for-each>
                                                        </td>
                                                        <td style="min-width:500px; text-align:left; ">
                                                            <xsl:for-each select="mstns:Path">
                                                                <xsl:apply-templates />
                                                            </xsl:for-each>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <div style="display:none; margin-left:10px; " id="{concat( &quot;files&quot;, mstns:ID )}">
                                                <table style="font-size: 12px; width:100%; " border="0" cellpadding="3">
                                                    <tbody>
                                                        <tr>
                                                            <td style="font-family:verdana; width:80px; ">
                                                                <span style="font-size:12px; ">
                                                                    <xsl:text>Rule</xsl:text>
                                                                </span>
                                                            </td>
                                                            <td style="font-family:verdana; min-width:780px; ">
                                                                <span>
                                                                    <xsl:text>Description</xsl:text>
                                                                </span>
                                                            </td>
                                                            <td style="font-family:verdana; width:50px; ">
                                                                <span>
                                                                    <xsl:text>Line</xsl:text>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <xsl:for-each select="mstns:Violations">
                                                    <xsl:for-each select="mstns:Rules">
                                                        <table style="border:0; cursor:pointer; font-family:verdana; font-size:12px; width:100%; " border="0" cellpadding="3" onclick="{concat( concat( concat( &quot;var el = document.getElementById(&apos;&quot; ,  concat(  mstns:CheckId  ,  ../mstns:ID  ) ), &quot;&apos;); if ( el.style.display==&apos;block&apos; ) el.style.display=&apos;none&apos;; else el.style.display=&apos;block&apos;;&quot; ), concat( concat( &quot;var el = document.getElementById(&apos;line&quot; ,  concat(  mstns:CheckId  ,  ../mstns:ID  ) ), &quot;&apos;); if ( el.style.display==&apos;block&apos; ) el.style.display=&apos;none&apos;; else el.style.display=&apos;block&apos;;&quot; ) )}">
                                                            <tbody>
                                                                <tr style="background-color:silver; ">
                                                                    <td style="color:blue; width:80px; ">
                                                                        <xsl:for-each select="mstns:CheckId">
                                                                            <xsl:apply-templates />
                                                                        </xsl:for-each>
                                                                    </td>
                                                                    <td style="min-width:780px; ">
                                                                        <span style="color:black; font-family:Verdana; ">
                                                                            <xsl:value-of select="../mstns:Message" />
                                                                        </span>
                                                                    </td>
                                                                    <td style="width:50px; ">
                                                                        <span>
                                                                            <xsl:value-of select="../mstns:Line" />
                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <div style="background-color:#ffffe9; display:none; font-family:verdana; font-size:12px; margin-left:10px; padding:3px; width:100%; " id="{concat( &quot;line&quot;, concat(  mstns:CheckId  ,  ../mstns:ID  ) )}">
                                                            <span>
                                                                <xsl:value-of select="../mstns:SourceCodeLine" />
                                                            </span>
                                                        </div>
                                                        <table style="background-color:#dee7ec; border:0; display:none; font-family:verdana; font-size:12px; margin-left:12px; width:100%; " border="0" cellpadding="3" id="{concat(  mstns:CheckId  ,  ../mstns:ID  )}">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width:80px; ">
                                                                        <span style="font-weight:bold; ">
                                                                            <xsl:text>Name</xsl:text>
                                                                        </span>
                                                                        <span>
                                                                            <xsl:text>:</xsl:text>
                                                                        </span>
                                                                    </td>
                                                                    <td style="min-width:820px; ">
                                                                        <xsl:for-each select="mstns:Name">
                                                                            <xsl:apply-templates />
                                                                        </xsl:for-each>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:80px; ">
                                                                        <span style="font-weight:bold; ">
                                                                            <xsl:text>Namespace</xsl:text>
                                                                        </span>
                                                                        <span>
                                                                            <xsl:text>:</xsl:text>
                                                                        </span>
                                                                    </td>
                                                                    <td style="min-width:820px; ">
                                                                        <xsl:for-each select="mstns:Namespace">
                                                                            <xsl:apply-templates />
                                                                        </xsl:for-each>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:80px; ">
                                                                        <span style="font-weight:bold; ">
                                                                            <xsl:text>Description</xsl:text>
                                                                        </span>
                                                                        <span>
                                                                            <xsl:text>:</xsl:text>
                                                                        </span>
                                                                    </td>
                                                                    <td style="min-width:820px; ">
                                                                        <xsl:for-each select="mstns:Description">
                                                                            <xsl:apply-templates />
                                                                        </xsl:for-each>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </xsl:for-each>
                                                </xsl:for-each>
                                            </div>
                                        </xsl:for-each>
                                    </div>
                                </xsl:for-each>
                            </div>
                        </xsl:for-each>
                    </xsl:for-each>
                </xsl:for-each>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
