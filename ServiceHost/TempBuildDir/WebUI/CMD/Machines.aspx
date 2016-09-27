﻿<%@ page language="C#" autoeventwireup="true" inherits="WebUI_CMD_Machines, WMS_deploy" theme="Default" stylesheettheme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Css/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Css/op.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" src="../../JQuery/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src= "../../JScript/Common.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $(window).resize(function () {
                resize();
            });
        });
        function resize() {
            var h = document.documentElement.clientHeight - 58;
            $("#table-container").css("height", h);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />  
        <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>            
                <div id="progressBackgroundFilter" style="display:none"></div>
                <div id="processMessage"> Loading...<br /><br />
                        <img alt="Loading" src="../../images/loading.gif" />
                </div>      
            </ProgressTemplate> 
        </asp:UpdateProgress>  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">                
            <ContentTemplate>
                <div>
                    <table  style="width: 100%; height: 20px;">
                    <tr>
						    <td class="smalltitle" align="center" width="7%" >
                                <asp:Literal ID="Literal1" Text="查询栏位" runat="server"  ></asp:Literal>
                             </td>
						    <td  width="15%" height="20">&nbsp;
                                <asp:dropdownlist id="ddlField" runat="server" Width="85%" >
                                    <asp:ListItem Selected="True" Value="ProductTypeName">产品类别</asp:ListItem>
                                    <asp:ListItem  Value="AxieNo">单位</asp:ListItem>
                                    <asp:ListItem  Value="ProductName">品名</asp:ListItem>
                                    <asp:ListItem Value="Memo">备注</asp:ListItem>
                                 </asp:dropdownlist>
                            </td>
						    <td class="smalltitle" align="center" width="7%">
                                <asp:Literal ID="Literal2" Text="查询内容" runat="server"></asp:Literal>
                            </td>
						    <td  width="26%" height="20" valign="middle">&nbsp;<asp:textbox id="txtSearch" 
                                    tabIndex="1" runat="server" Width="90%" CssClass="TextBox"  
                                    heigth="16px" ></asp:textbox>
                               
                          </td>
                          <td width="15%" align="left">
                           &nbsp;<asp:button id="btnSearch" tabIndex="2" runat="server" Width="58px" 
                                    CssClass="ButtonQuery" Text="查询" OnClientClick="return Search()" 
                                    onclick="btnSearch_Click"></asp:button>&nbsp;&nbsp;
                              <asp:Button ID="btnRefresh" runat="server" CssClass="ButtonRefresh" 
                                  onclick="btnSearch_Click" OnClientClick="return Refresh()" tabIndex="2" 
                                  Text="刷新" Width="58px" />
                          
                          </td>
                          <td align="right"  style="width:30%" valign="middle">
                             <%-- <asp:Button ID="btnPrint" runat="server" Text="导出" CssClass="ButtonPrint" OnClientClick="return print();"/>--%>
                           
                            <asp:Button ID="btnAdd" runat="server" Text="新增" OnClientClick="return Add();" CssClass="ButtonCreate"/>&nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" onclick="btnDeletet_Click" OnClientClick="return Delete('GridView1')" Width="51px"/>&nbsp;
                             <asp:Button ID="btnPrint" runat="server" CssClass="ButtonPrint"   Text="打印" />&nbsp;
                            <asp:Button ID="btnExit" runat="server" Text="离开" CssClass="ButtonExit" OnClientClick="return Exit()" Width="51px" />&nbsp;&nbsp;
                            
                          </td>
                    </tr>
                </table>
                    
                </div>
                <div id="table-container" style="overflow: auto; WIDTH: 100%; HEIGHT: 470px">
                
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" SkinID="GridViewSkin" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField >
                        <HeaderTemplate>
                        <input type="checkbox" onclick="selectAll('GridView1',this.checked);" />                    
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:CheckBox id="cbSelect" runat="server" ></asp:CheckBox>                   
                        </ItemTemplate>
                      <HeaderStyle Width="60px"></HeaderStyle>
                     <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                   </asp:TemplateField>
                  <asp:TemplateField HeaderText="产品编号" SortExpression="ProductCode">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# FormID+"View.aspx?SubModuleCode=" + SubModuleCode+"&FormID=" + FormID +"&SqlCmd="+SqlCmd+ "&ID="+DataBinder.Eval(Container.DataItem, "ProductCode") %>'
                                Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle Width="12%" Wrap="False" />
                        <HeaderStyle Width="12%" Wrap="False" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProductName" HeaderText="品名" SortExpression="ProductName">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductTypeName" HeaderText="产品类别" SortExpression="ProductTypeName">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                     
                     <asp:CheckBoxField DataField="ChkIsTmp" HeaderText="临时产品"  SortExpression="ChkIsTmp">
                        <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:CheckBoxField>
                    
                    <asp:BoundField DataField="AxieNo" HeaderText="单位" SortExpression="AxieNo">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="WheelDiameter" HeaderText="额定功率" SortExpression="WheelDiameter">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CCZ_Diameter" HeaderText="额定电压" SortExpression="CCZ_Diameter">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FCCZ_Diameter" HeaderText="额定电流" SortExpression="FCCZ_Diameter">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CCD_Diameter" HeaderText="额定转速" SortExpression="CCD_Diameter">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FCCD_Diameter" HeaderText="绝缘等级" SortExpression="FCCD_Diameter">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CCXPBZW_Size" HeaderText="工作制" SortExpression="CCXPBZW_Size">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FCCXPBZW_Size" HeaderText="最高电压" SortExpression="FCCXPBZW_Size">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                     <asp:BoundField DataField="GearNo" HeaderText="最大电流" SortExpression="GearNo">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CCLX_Flag" HeaderText="最大转速" SortExpression="CCLX_Flag">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                     <asp:BoundField DataField="FCCLX_Flag" HeaderText="励磁方式" SortExpression="FCCLX_Flag">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="CCLX_Year" HeaderText="电机重量" SortExpression="CCLX_Year">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                     <asp:BoundField DataField="FCCLX_Year" HeaderText="调压范围" SortExpression="FCCLX_Year">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>

                    
                     <asp:BoundField DataField="CXFactoryName" HeaderText="承修厂家" SortExpression="CXFactoryName">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                     <asp:BoundField DataField="CCLG_Flag" HeaderText="调流范围" SortExpression="CCLG_Flag">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                   
                    <asp:BoundField DataField="LDXC" HeaderText="轮对修程" SortExpression="LDXC">
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="承修时间" SortExpression="CX_DateTime">
                        <ItemTemplate>
                            <%# ToYMD(DataBinder.Eval(Container.DataItem, "CX_DateTime"))%>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="入库时间" SortExpression="InStockDate">
                        <ItemTemplate>
                            <%# ToYMD(DataBinder.Eval(Container.DataItem, "InStockDate"))%>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="AreaName" HeaderText="库区" 
                        SortExpression="AreaName" >
                        <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Memo" HeaderText="备注" 
                        SortExpression="Memo" >
                        <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Creator" HeaderText="建单人员" 
                        SortExpression="Creator"  >
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="建单日期" SortExpression="CreateDate">
                        <ItemTemplate>
                            <%# ToYMD(DataBinder.Eval(Container.DataItem, "CreateDate"))%>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Updater" HeaderText="修改人员" 
                        SortExpression="Updater"  >
                        <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="修改日期" SortExpression="UpdateDate">
                        <ItemTemplate>
                            <%# ToYMD(DataBinder.Eval(Container.DataItem, "UpdateDate"))%>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                    </asp:TemplateField>
               
                </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
            </div>
                <div>
                   &nbsp;&nbsp;<asp:LinkButton ID="btnFirst" runat="server" OnClick="btnFirst_Click" Text="首页"></asp:LinkButton> 
                    &nbsp;<asp:LinkButton ID="btnPre" runat="server" OnClick="btnPre_Click" Text="上一页"></asp:LinkButton> 
                    &nbsp;<asp:LinkButton ID="btnNext" runat="server" OnClick="btnNext_Click" Text="下一页"></asp:LinkButton> 
                    &nbsp;<asp:LinkButton ID="btnLast" runat="server" OnClick="btnLast_Click" Text="尾页"></asp:LinkButton> 
                    &nbsp;<asp:textbox id="txtPageNo" onkeypress="return regInput(this,/^\d+$/,String.fromCharCode(event.keyCode))"
					        onpaste="return regInput(this,/^\d+$/,window.clipboardData.getData('Text'))" ondrop="return regInput(this,/^\d+$/,event.dataTransfer.getData('Text'))"
					        runat="server" Width="56px" CssClass="TextBox" ></asp:textbox>
                    &nbsp;<asp:linkbutton id="btnToPage" runat="server" onclick="btnToPage_Click" Text="跳转"></asp:linkbutton>
                    &nbsp;<asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" onselectedindexchanged="ddlPageSize_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                    &nbsp;<asp:Label ID="lblCurrentPage" runat="server" ></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
