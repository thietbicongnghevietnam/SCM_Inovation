<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportBorrowReturn.aspx.cs" Inherits="FreeLayout.ReportBorrowReturn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Borrow & return</title>	
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>		
	<style type="text/css">
		body,div,table,thead,tbody,tfoot,tr,th,td,p { font-family:"Calibri"; font-size:x-small }
		a.comment-indicator:hover + comment { background:#ffd; position:absolute; display:block; border:1px solid black; padding:0.5em;  } 
		a.comment-indicator { background:red; display:inline-block; border:1px solid black; width:0.5em; height:0.5em;  } 
		comment { display:none;  } 
	</style>

</head>
<body>

            <div>
                <table cellspacing="0" border="0">
	<colgroup width="36"></colgroup>
	<colgroup width="39"></colgroup>
	<colgroup width="73"></colgroup>
	<colgroup width="45"></colgroup>
	<colgroup span="9" width="79"></colgroup>
	<colgroup width="45"></colgroup>
	<colgroup span="2" width="79"></colgroup>
	<colgroup span="3" width="45"></colgroup>
	<colgroup span="2" width="79"></colgroup>
	<colgroup width="178"></colgroup>
	<tr>
		<td colspan=22 height="43" align="center" valign=middle bgcolor="#FFFFFF"><b><u><font face="Meiryo UI" size=6>Borrow &amp; Carry out Equipment Control Sheet</font></u></b></td>
		</tr>
	<tr>
		<td colspan=22 height="29" align="center" valign=middle bgcolor="#FFFFFF"><b><u><font face="Tahoma" size=5>Bảng quản lý cho mượn thiết bị </font></u></b></td>
		</tr>
</table>
                    <div style="width:500px; margin-left:50px; float:left; margin-top:20px;">
                        <div style="margin-bottom:10px;"><b style="font-size:14px;">Section/Bộ phận :  ISD </b></div>
                            <div style="margin-bottom:10px;"><b style="font-size:14px;">Device name/Tên Thiết bị: <%=itemid %></b></div>
                     </div>
                     <div style="width:500px; margin-left:300px; float:left; margin-top:20px;">
                            <div style="margin-bottom:10px;"><b style="font-size:14px;">PIC/Phụ trách :</b></div>
                            <div style="margin-bottom:10px;"><b style="font-size:14px;">Device Nc/Mã số quản lý thiết bị: <%=serialno %></b></div>
                     </div>
             </div>

    <form id="form1" runat="server">
                             
        <div>
            <table cellspacing="0" border="0">
	<colgroup width="36"></colgroup>
	<colgroup width="39"></colgroup>
	<colgroup width="73"></colgroup>
	<colgroup width="45"></colgroup>
	<colgroup span="9" width="79"></colgroup>
	<colgroup width="45"></colgroup>
	<colgroup span="2" width="79"></colgroup>
	<colgroup span="3" width="45"></colgroup>
	<colgroup span="2" width="79"></colgroup>
	<colgroup width="178"></colgroup>
	
               	
	<tr>
		<td height="13" align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
		<td align="center" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック"><br></font></td>
	</tr>
	<tr>
		<td style="border-top: 2px solid #000000; border-bottom: 2px solid #000000; border-left: 2px solid #000000; border-right: 2px solid #000000" rowspan=5 height="242" align="center" valign=middle bgcolor="#FFFFFF"><font face="Tahoma" size=1 color="#000000">No<br>(Số)</font></td>
		<td style="border-top: 2px solid #000000; border-bottom: 1px solid #000000; border-left: 2px solid #000000; border-right: 2px solid #000000" colspan=11 align="center" valign=middle bgcolor="#EDEDED"><font face="ＭＳ Ｐゴシック" color="#000000">Before Borrow</font></td>
		<td style="border-top: 2px solid #000000; border-bottom: 1px solid #000000; border-left: 2px solid #000000; border-right: 2px solid #000000" colspan=9 align="center" valign=middle bgcolor="#E2F0D9"><font face="ＭＳ Ｐゴシック" color="#000000">When Return</font></td>
		<td style="border-top: 2px solid #000000; border-bottom: 2px solid #000000; border-right: 2px solid #000000" rowspan=5 align="center" valign=middle><font face="Tahoma" color="#000000">Remark</font></td>
	</tr>
	<tr>
		<td style="border-right: 1px solid #000000" colspan=5 align="center" valign=middle bgcolor="#EDEDED"><font face="ＭＳ Ｐゴシック" color="#000000">Controller</font></td>
		<td style="border-left: 1px solid #000000; border-right: 2px solid #000000" colspan=6 align="center" valign=middle bgcolor="#EDEDED"><font face="ＭＳ Ｐゴシック" color="#000000">Borrower</font></td>
		<td style="border-top: 1px solid #000000; border-left: 2px solid #000000; border-right: 1px solid #000000" colspan=4 align="center" valign=middle bgcolor="#E2F0D9"><font face="ＭＳ Ｐゴシック" color="#000000">Returner</font></td>
		<td style="border-top: 1px solid #000000; border-left: 1px solid #000000; border-right: 2px solid #000000" colspan=5 align="center" valign=middle bgcolor="#E2F0D9"><font face="ＭＳ Ｐゴシック" color="#000000">Controller</font></td>
		</tr>
	<tr>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 2px solid #000000; border-right: 1px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">Virus scan<br>(Quét virus)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" colspan=4 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">Confirm/ Xác nhận</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">Borrow<br>Date<br>(Ngày mượn)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">Purpose<br>(Mục đích)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">Return<br>Plan<br>Ngày dự định trả</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">ID No<br>(Số ID)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">Full name<br>(Họ tên)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 2px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">Section<br>(Bộ phận)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 2px solid #000000; border-right: 1px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#E2F0D9"><font face="Tahoma" size=1 color="#000000">Return<br>Date<br>(Ngày trả)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#E2F0D9"><font face="Tahoma" size=1 color="#000000">Delete<br>Data<br>(đã xóa dữ liệu)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#E2F0D9"><font face="Tahoma" size=1 color="#000000">ID No<br>(Số ID)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#E2F0D9"><font face="Tahoma" size=1 color="#000000">Full name<br>(Họ tên)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=3 align="center" valign=middle bgcolor="#E2F0D9"><font face="Tahoma" size=1 color="#000000">Virus scan<br>(Quét virus)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 2px solid #000000" colspan=4 align="center" valign=middle bgcolor="#E2F0D9"><font face="Tahoma" size=1 color="#000000">Confirm/ Xác nhận</font></td>
		</tr>
	<tr>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=2 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">Have a Media drive follow note 3<br>(Có hiển thị ổ Media thực hiện theo ghi chú 3)<br>yes/no<br>Thực hiện theo ghi chú 3 </font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=2 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">No Data<br>(Không có dữ liệu)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=2 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">ID No<br>(Số ID)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=2 align="center" valign=middle bgcolor="#EDEDED"><font face="Tahoma" size=1 color="#000000">Full name<br>(Họ tên)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=2 align="center" valign=middle bgcolor="#E2F0D9"><font face="Tahoma" size=1 color="#000000">Have a Media drive<br>(Có hiển thị ổ Media)<br>yes/no</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=2 align="center" valign=middle bgcolor="#E2F0D9"><font face="Tahoma" size=1 color="#000000">No Data<br>(Không có dữ liệu)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" rowspan=2 align="center" valign=middle bgcolor="#E2F0D9"><font face="Tahoma" size=1 color="#000000">ID No<br>(Số ID)</font></td>
		<td style="border-top: 1px solid #000000; border-bottom: 2px solid #000000; border-left: 1px solid #000000; border-right: 2px solid #000000" rowspan=2 align="center" valign=middle bgcolor="#E2F0D9"><font face="Tahoma" size=1 color="#000000">Full name<br>(Họ tên)</font></td>
		</tr>
	<tr>
		</tr>
                <%int i = 0; %>
                                    <%foreach (System.Data.DataRow rows in dt_report.Rows)
                                        {%>
                                     <%i++;%>
	<tr>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="20001" sdnum="1033;"><font face="ＭＳ Ｐゴシック" color="#000000"><%=i %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="20001" sdnum="1033;">OK</font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="20001" sdnum="1033;">Yes</font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="20001" sdnum="1033;">OK</font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="20001" sdnum="1033;"><font face="Tahoma" size=1 color="#000000"><%=rows["CreateUser"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="20001" sdnum="1033;"><%=rows["fullname"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="20001" sdnum="1033;"><font face="Tahoma" size=1 color="#000000"><%=rows["BORROWTIME"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="20001" sdnum="1033;"><font face="Tahoma" size=1 color="#000000"><%=rows["Reason"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="20001" sdnum="1033;"><font face="Tahoma" size=1 color="#000000"><%=rows["RETURNERTIME"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="20001" sdnum="1033;"><font face="Tahoma" size=1 color="#000000"><%=rows["UserIDMuon"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9"><font face="Tahoma" size=1 color="#000000"><%=rows["fullname2"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 2px solid #000000" align="center" valign=middle bgcolor="#D9D9D9"><font face="Tahoma" size=1 color="#000000"><%=rows["Department"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 2px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9"><font face="Tahoma" size=1 color="#000000"><%=rows["CreateUser_update"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9"><font face="Tahoma" size=1 color="#000000">OK</font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="20001" sdnum="1033;"><font face="Tahoma" size=1 color="#000000"><%=rows["UserIDTra"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9"><font face="Tahoma" size=1 color="#000000"><%=rows["fullname3"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9"><font face="Tahoma" size=1 color="#000000">OK</font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9"><font face="Tahoma" size=1 color="#000000">Yes</font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9"><font face="Tahoma" size=1 color="#000000">OK</font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 1px solid #000000" align="center" valign=middle bgcolor="#D9D9D9" sdval="200000" sdnum="1033;"><font face="Tahoma" size=1 color="#000000"><%=rows["CreateUser_update"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-left: 1px solid #000000; border-right: 2px solid #000000" align="center" valign=middle bgcolor="#D9D9D9"><font face="Tahoma" size=1 color="#000000"><%=rows["fullname4"].ToString() %></font></td>
		<td style="border-bottom: 2px double #000000; border-right: 2px solid #000000" align="center" valign=middle bgcolor="#D9D9D9"><font face="Tahoma" size=1 color="#000000"></font></td>
	</tr>
                <%} %>
	
	<tr>
		<td height="9" align="right" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
	</tr>
                <tr style="border:1px solid">
		<td height="9" align="right" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="center" valign=middle sdnum="1033;0;M/D/YYYY"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
	</tr>
	<tr>
		<td height="28" align="left" valign=middle bgcolor="#FFFFFF"><b><font face="ＭＳ Ｐゴシック" color="#000000">Note: </font></b></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="left" valign=middle bgcolor="#FFFFFF"><font face="ＭＳ Ｐゴシック" color="#000000"><br></font></td>
		<td align="right" valign=middle bgcolor="#FFFFFF"><font face="Tahoma" color="#000000">ISM-P-020-FORM 03 Ver 2</font></td>
	</tr>
                			
</table>
            <table>
                <tr>
                    <td style="margin-left:30px;">1. At the end of the month if it is not for compulsory reasons please return to the department control, then continue borrowing.</td>
                </tr>
                  <tr>
                    <td style="margin-left:30px;">Đến cuối tháng nếu không phải vì lý do bắt buộc thì hãy trả lại thiết bị media cho bộ phận quản lý, sau đó tiếp tục mượn.</td>
                </tr>
                <tr>
                    <td style="margin-left:30px;">2. When the payment is past the scheduled time, please update the reason and the next payment plan.</td>
                </tr>
                <tr>
                    <td style="margin-left:30px;">2. Khi đã quá thời gian dự định trả, thì vui lòng cập nhật lý do và thời gian dự định trả tiếp theo.</td>
                </tr>
                 <tr>
                    <td style="margin-left:30px;">3. Please refer &quot;Manual Scan Virus on Removable Media&quot; 1.Connect Network 2.Check Viurs Paturn 3. Remove Network 4. Open Explorer</td>
                </tr>
	            <tr>
                    <td style="margin-left:30px;">3. Vui lòng tham khảo hướng dẫn scan virus trên thiết bị media &quot;1. kết nối mạng, 2. kiểm tra việc cập nhật phần mềm diệt virus, 3. Ngắt kết nối mạng, 4. Bắt đầu kiểm tra hiển thị ổ Media</td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
