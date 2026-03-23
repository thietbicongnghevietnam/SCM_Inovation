<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintpartcardQC.aspx.cs" Inherits="FreeLayout.PrintpartcardQC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print partcard scrap tool</title>
        <link rel="stylesheet" href="/plugins/fontawesome-free/css/all.min.css" />

    <link rel="stylesheet" href="/plugins/datatables-responsive/css/responsive.bootstrap4.min.css" />
    <link rel="stylesheet" href="/plugins/toastr/toastr.css" />
    <link rel="stylesheet" href="/dist/css/adminlte.min.css" />
    <link rel="stylesheet" href="/plugins/fontawesome-free/css/jquery-ui.css" />

    <link rel="stylesheet" href="/dist/Infra/bootstrap.css" />
    <link rel="stylesheet" href="/dist/Infra/dataTables.bootstrap4.min.css" />

    <script src="/plugins/jquery/jquery.min.js"></script>
    <script src="/plugins/toastr/toastr.js"></script>

    <script src="/dist/Infra/jquery-3.7.0.js"></script>
    <script src="/dist/Infra/jquery.dataTables.min.js"></script>
    <script src="/dist/Infra/dataTables.bootstrap4.min.js"></script>
    <script src="/Exportexcel/jquery.table2excel.min.js"></script>

        <style>
    .yes-label {
        background: #28a745;
        color: white;
        padding: 3px 8px;
        border-radius: 4px;
    }
    .no-label {
        background: #6c757d;
        color: white;
        padding: 3px 8px;
        border-radius: 4px;
    }

    @media print {
    @page {
        size: A4;
        margin: 10mm;
    }
}

.container {
    display: flex;
    flex-wrap: wrap;
    gap: 10px;
}

.card {
    width: 24%; /* 4 card / dòng */
    box-sizing: border-box;
}

.card table {
    width: 100%;
    border-collapse: collapse;
    font-size: 12px;
}

.card td {
    border: 1px solid #000;
    padding: 4px;
}
</style>


</head>

<body>
    <form id="form1" runat="server">
    <div>

        <div class="row" style="width: 100%">

           <div class="container">

    <!-- CARD 1 -->
                <%int i = 0; %>
 <%foreach (System.Data.DataRow rows in dt_scrap.Rows)
     {%>
 <%i++;%>
    <div class="card">
        <table>
            <tr>
                <td style="text-align:center;"><b></b></td>
                <td colspan="2" style="text-align:center;"><b>Part card</b></td>
            </tr>
            <tr>
                <td>STT</td>
                <td colspan="2"><%= rows["Noid"].ToString() %></td>
            </tr>
            <tr>
                <td>Part NO</td>
                <td colspan="2"><%= rows["Material"].ToString() %></td>
            </tr>
            <tr>
                <td>Part Name</td>
                <td colspan="2"><%= rows["partname"].ToString() %></td>
            </tr>
            <tr>
                <td>Qty</td>
                <td colspan="2" style="text-align:right"><%= rows["Qty"].ToString() %></td>
            </tr>
            <tr>
                <td>PalletNO</td>
                <td colspan="2" style="text-align:center"><%= rows["Pallet"].ToString() %></td>
            </tr>
            <tr>
                <td>Box</td>
                <td colspan="2" style="text-align:center"><%= rows["BoxNO"].ToString() %></td>
            </tr>
        </table>
    </div>
                <%} %>
               <div class="row" style="margin-top: 20px; margin-left:100px;">   </div>

    <!-- COPY thêm 3 cái nữa : for ...-->
   

</div>

        </div>


    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            window.addEventListener("load", window.print());
        })
        //$('#btnPrint').click(function () {
        //    window.addEventListener("load", window.print());
        //});

    </script>

</form>
</body>

</html>
