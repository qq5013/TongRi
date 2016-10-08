using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class OrderDao: BaseDao
    {
        public DataTable FindMasterAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT A.*,B.ROUTENAME,C.CUSTOMERNAME FROM SC_I_ORDERMASTER A " +
                "LEFT JOIN CMD_ROUTE B ON A.ROUTECODE=B.ROUTECODE " +
                "LEFT JOIN CMD_CUSTOMER C ON A.CUSTOMERCODE=C.CUSTOMERCODE " + where;
            sql += " ORDER BY ORDERDATE,BATCHNO, ORDERID";
            return ExecuteQuery(sql, "SC_I_ORDERMASTER", startRecord, pageSize).Tables[0];
        }

        public int FindMasterCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM SC_I_ORDERMASTER A ";
            sql += where;
            return (int)ExecuteScalar(sql);
        }

        public DataTable FindDetailAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT *,QUANTITY/50 JQUANTITY,QUANTITY%50 TQUANTITY FROM SC_I_ORDERDETAIL " + where;
            sql += " ORDER BY QUANTITY DESC";
            return ExecuteQuery(sql, "SC_I_ORDERDETAIL", startRecord, pageSize).Tables[0];
        }

        public int FindDetailCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM SC_I_ORDERDETAIL ";
            sql += where;
            return (int)ExecuteScalar(sql);
        }


        public DataTable FindRouteAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT A.*, C.AREANAME, D.ROUTENAME, CASE WHEN B.QUANTITY % 25 = 0 THEN B.QUANTITY / 25 ELSE B.QUANTITY / 25 + 1 END QUANTITY " +
                "FROM SC_I_ORDERMASTER A LEFT JOIN (SELECT ORDERID, SUM(QUANTITY) QUANTITY FROM SC_I_ORDERDETAIL WHERE ProductCode NOT IN " +
                "(SELECT ProductCode FROM CMD_Product WHERE ISABNORMITY='1') GROUP BY ORDERID) B ON A.ORDERID=B.ORDERID " +
                "LEFT JOIN CMD_AREA C ON A.AREACODE=C.AREACODE LEFT JOIN CMD_ROUTE D ON A.ROUTECODE=D.ROUTECODE ";
            sql += where;
            sql += " ORDER BY ORDERDATE,BATCHNO,AREACODE,ROUTECODE,SORTID";
            return ExecuteQuery(sql, "AS_BI_ORDER", startRecord, pageSize).Tables[0];
        }

        public int FindRouteCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM (SELECT A.*, C.AREANAME, D.ROUTENAME, CASE WHEN B.QUANTITY % 25 = 0 THEN B.QUANTITY / 25 ELSE B.QUANTITY / 25 + 1 END QUANTITY " +
                "FROM SC_I_ORDERMASTER A LEFT JOIN (SELECT ORDERID, SUM(QUANTITY) QUANTITY FROM SC_I_ORDERDETAIL WHERE ProductCode NOT IN " +
                "(SELECT ProductCode FROM CMD_Product WHERE ISABNORMITY='1') GROUP BY ORDERID) B ON A.ORDERID=B.ORDERID " +
                "LEFT JOIN CMD_AREA C ON A.AREACODE=C.AREACODE LEFT JOIN CMD_ROUTE D ON A.ROUTECODE=D.ROUTECODE) E ";
            sql += where;
            return (int)ExecuteScalar(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataSet FindRouteQuantity(string batchNo)
        {
            //排除异形烟
            string sql = "SELECT ROUTECODE, SORTID, SUM(QUANTITY) QUANTITY " +
                            " FROM (SELECT A.ROUTECODE, C.SORTID, B.QUANTITY " +
                            " 		FROM SC_I_ORDERMASTER A " +
                            " 		LEFT JOIN SC_I_ORDERDETAIL B " +
                            " 			ON A.ORDERID = B.ORDERID " +
                            " 		LEFT JOIN CMD_ROUTE C " +
                            " 			ON A.ROUTECODE = C.ROUTECODE " +
                            " 		WHERE A.BATCHNO = '{0}' " +
                            " 		AND ProductCode NOT IN " +
                            " 		(SELECT ProductCode " +
                            " 			FROM CMD_Product " +
                            " 			WHERE ISABNORMITY = '1')) D " +
                            " GROUP BY ROUTECODE, SORTID ORDER BY SORTID,QUANTITY DESC";
            return ExecuteQuery(string.Format(sql, batchNo));
        }

        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataSet FindRouteQuantity(string orderDate, string batchNo)
        {
            //排除异形烟
            string sql = "SELECT ROUTECODE, SORTID, SUM(QUANTITY) QUANTITY" +
                            " FROM (SELECT A.ROUTECODE, C.SORTID, B.QUANTITY " +
                            " 		FROM SC_I_ORDERMASTER A " +
                            " 		LEFT JOIN SC_I_ORDERDETAIL B " +
                            " 			ON A.ORDERID = B.ORDERID " +
                            " 		LEFT JOIN CMD_ROUTE C " +
                            " 			ON A.ROUTECODE = C.ROUTECODE " +
                            " 		LEFT JOIN CMD_AREA D" +
                            " 			ON C.AREACODE=D.AREACODE " +
                            " 		WHERE A.ORDERDATE = '{0}' AND A.BATCHNO = '{1}' " +
                            " 		AND ProductCode NOT IN " +
                            " 		(SELECT ProductCode " +
                            " 			FROM CMD_Product " +
                            " 			WHERE ISABNORMITY = '1')) D " +
                            " GROUP BY ROUTECODE, SORTID ORDER BY SORTID,QUANTITY DESC";
            return ExecuteQuery(string.Format(sql, orderDate, batchNo));
        }

        /// <summary>
        /// 分拣货仓优化时，取卷烟名称及数量，进行优化。[ZENG]
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet FindCigaretteQuantity(string orderDate, string batchNo, string lineCode)
        {
            //排除异形烟
            string sql = "SELECT A.ProductCode, B.ProductName, " +
                            " SUM(A.QUANTITY) QUANTITY, SUM(A.QUANTITY -A.QUANTITY % 5) QUANTITY5" +
                            " FROM SC_I_ORDERDETAIL A" +
                            " LEFT JOIN CMD_Product B ON A.ProductCode = B.ProductCode" +
                            " LEFT JOIN SC_I_ORDERMASTER C ON A.ORDERID = C.ORDERID" +
                            " LEFT JOIN SC_LINE D ON A.ORDERDATE = D.ORDERDATE AND C.BATCHNO = D.BATCHNO AND C.ROUTECODE = D.ROUTECODE" +
                            " WHERE B.ISABNORMITY != '1' AND A.ORDERDATE='{0}' AND C.BATCHNO = '{1}' AND D.LINECODE = '{2}'" +
                            " GROUP BY A.ProductCode, B.ProductName " +
                            " ORDER BY SUM(A.QUANTITY) DESC";

            return ExecuteQuery(string.Format(sql, orderDate, batchNo, lineCode));
        }

        /// <summary>
        /// 补货货仓优化时，取卷烟名称及数量，进行优化。[ZENG]
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindCigaretteQuantity(string orderDate, string batchNo)
        {
            //排除异形烟
            string sql = "SELECT A.ProductCode,B.ProductName, SUM(QUANTITY) QUANTITY" +
                            " FROM SC_I_ORDERDETAIL A" +
                            " LEFT JOIN CMD_Product B ON A.ProductCode =B.ProductCode" +
                            " LEFT JOIN SC_I_ORDERMASTER C ON A.ORDERID = C.ORDERID" +
                            " WHERE B.ISABNORMITY != '1' AND A.ORDERDATE='{0}' AND C.BATCHNO = '{1}' " +
                            " GROUP BY A.ProductCode, B.ProductName HAVING SUM(QUANTITY) >= 50 " +
                            " ORDER BY SUM(QUANTITY) DESC";

            return ExecuteQuery(string.Format(sql, orderDate, batchNo)).Tables[0];
        }
        /// <summary>
        /// 分拣货仓优化时，取卷烟名称及数量，进行优化。[ZENG 2010-11-19]
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataSet FindAllCigaretteQuantity(string batchNo, bool isUseWholePiecesSortLine)
        {
            //排除异形烟和不分拣的线路
            string sql = "SELECT PRODUCTCODE, PRODUCTNAME, SUM(QUANTITY) QUANTITY, SUM(QUANTITY - QUANTITY % 5) QUANTITY5 " +
                "FROM SC_I_ORDERDETAIL WHERE " +
                "PRODUCTCODE NOT IN (SELECT PRODUCTCODE FROM CMD_Product WHERE ISABNORMITY = '1') AND " +
                "ORDERID IN (SELECT ORDERID FROM SC_I_ORDERMASTER A WHERE A.BATCHNO = '{0}' AND ROUTECODE IN(SELECT ROUTECODE FROM CMD_ROUTE R WHERE R.BATCHNO='{0}' AND R.ISSORT='1'))  " +
                "GROUP BY PRODUCTCODE, PRODUCTNAME ORDER BY SUM(QUANTITY) DESC";

            sql = "SELECT * FROM V_ORDER_TOTAL T WHERE T.BATCHNO = '{0}' ORDER BY TOTAL_PRODUCT_QUANTITY DESC";
            
            return ExecuteQuery(string.Format(sql, batchNo));
        }
        /// <summary>
        /// 分拣货仓优化时，取卷烟名称及数量，进行优化。[ZENG 2010-11-19]
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataSet FindAllCigaretteQuantity(string orderDate, string batchNo, bool isUseWholePiecesSortLine)
        {
            //排除异形烟
            string sql = "SELECT ProductCode, ProductNAME, SUM(QUANTITY) QUANTITY, SUM(QUANTITY - QUANTITY % 5) QUANTITY5 " +
                "FROM SC_I_ORDERDETAIL WHERE " +
                "ProductCode NOT IN (SELECT ProductCode FROM CMD_Product WHERE ISABNORMITY = '1') AND " +
                "ORDERID IN ( SELECT ORDERID FROM SC_I_ORDERMASTER A WHERE A.ORDERDATE='{0}' AND A.BATCHNO = '{1}' )  " +
                "GROUP BY ProductCode, ProductNAME ORDER BY SUM(QUANTITY) DESC";

            sql = "SELECT * FROM V_ORDER_TOTAL T WHERE T.ORDERDATE='{0}' AND T.BATCHNO = '{1}' ORDER BY TOTAL_PRODUCT_QUANTITY DESC";
//            if (isUseWholePiecesSortLine)
//            {
//                sql = @"SELECT ProductCode, ProductName, SUM(QUANTITY) QUANTITY, SUM(QUANTITY - QUANTITY % 5) QUANTITY5
//                            FROM  (
//                            SELECT ORDERID,ProductCode, ProductName, SUM(QUANTITY)%50 QUANTITY, SUM(QUANTITY - QUANTITY % 5) QUANTITY5 
//                            FROM SC_I_ORDERDETAIL 
//                            WHERE ProductCode NOT IN (SELECT ProductCode FROM CMD_Product WHERE ISABNORMITY = '1') 
//                            AND ORDERID IN ( SELECT ORDERID FROM SC_I_ORDERMASTER A WHERE A.ORDERDATE='{0}' AND A.BATCHNO = '{1}' )  
//                            GROUP BY ORDERID,ProductCode, ProductName HAVING SUM(QUANTITY)%50  > 0 ) Q
//                            GROUP BY ProductCode, ProductName
//                            ORDER BY SUM(QUANTITY) DESC";
//            }
            return ExecuteQuery(string.Format(sql, orderDate, batchNo));
        }

        /// <summary>
        /// 分拣货仓优化时，取卷烟名称及数量，进行优化。[ZENG 2010-11-19]
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet FindLineCigaretteQuantity(string orderDate, string batchNo, string lineCode, bool isUseWholePiecesSortLine)
        {
            //排除异形烟
            string sql = "SELECT ProductCode, ProductName, SUM(QUANTITY) QUANTITY, SUM(QUANTITY - QUANTITY % 5) QUANTITY5 " +
                "FROM SC_I_ORDERDETAIL WHERE " +
                "ProductCode NOT IN (SELECT ProductCode FROM CMD_Product WHERE ISABNORMITY = '1') AND " +
                "ORDERID IN ( SELECT ORDERID FROM SC_I_ORDERMASTER A LEFT JOIN SC_LINE B ON " +
                "A.ROUTECODE = B.ROUTECODE AND A.ORDERDATE = B.ORDERDATE AND A.BATCHNO = B.BATCHNO " +
                "WHERE A.ORDERDATE='{0}' AND A.BATCHNO = '{1}' AND B.LINECODE = '{2}' )  " +
                "GROUP BY ProductCode, ProductName ORDER BY SUM(QUANTITY) DESC";

            if (isUseWholePiecesSortLine)
            {
                sql = @"SELECT ProductCode, ProductName, SUM(QUANTITY) QUANTITY, SUM(QUANTITY - QUANTITY % 5) QUANTITY5
                            FROM  (
                            SELECT ORDERID,ProductCode, ProductName, SUM(QUANTITY)%50 QUANTITY, SUM(QUANTITY - QUANTITY % 5) QUANTITY5 
                            FROM SC_I_ORDERDETAIL 
                            WHERE ProductCode NOT IN (SELECT ProductCode FROM CMD_Product WHERE ISABNORMITY = '1') 
                            AND ORDERID IN ( SELECT ORDERID FROM SC_I_ORDERMASTER A 
                            LEFT JOIN SC_LINE B ON A.ROUTECODE = B.ROUTECODE AND A.ORDERDATE = B.ORDERDATE AND A.BATCHNO = B.BATCHNO
                            WHERE A.ORDERDATE='{0}' AND A.BATCHNO = '{1}' AND B.LINECODE = '{2}' )  
                            GROUP BY ORDERID,ProductCode, ProductName HAVING SUM(QUANTITY)%50  > 0 ) Q
                            GROUP BY ProductCode, ProductName
                            ORDER BY SUM(QUANTITY) DESC";
            }

            return ExecuteQuery(string.Format(sql, orderDate, batchNo, lineCode));
        }

        /// <summary>
        /// 备货货仓优化，取卷烟名称及数量，进行优化。[ZENG 2010-11-19]
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="channelType"></param>
        /// <returns></returns>
        public DataTable FindCigaretteQuantityFromChannelUsed(string orderDate, string batchNo, string channelType)
        {
            string sql = string.Format("SELECT ProductCode,ProductName,SUM(QUANTITY) QUANTITY ,MAX(CHANNELORDER) CHANNELORDER" +
                                            " FROM SC_CHANNELUSED " +
                                            " WHERE ORDERDATE='{0}' AND BATCHNO='{1}' AND CHANNELTYPE='{2}' AND ProductCode != '' AND QUANTITY >= 50 " +
                                            " GROUP BY ProductCode,ProductName ORDER BY SUM(QUANTITY) DESC,CHANNELORDER ",
                                            orderDate, batchNo, channelType);

            sql = string.Format("SELECT ProductCode,ProductNAME,SUM(QUANTITY) QUANTITY ,MAX(CHANNELORDER) CHANNELORDER" +
                                            " FROM SC_CHANNELUSED " +
                                            " WHERE ORDERDATE='{0}' AND BATCHNO='{1}' AND CHANNELTYPE='{2}' AND ProductCode != '' " +
                                            " GROUP BY ProductCode,ProductNAME ORDER BY SUM(QUANTITY) DESC,CHANNELORDER ",
                                            orderDate, batchNo, channelType);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 备货货仓优化，取卷烟名称及数量，进行优化。
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="channelType"></param>
        /// <returns></returns>
        public DataTable FindCigaretteQuantityFromChannelUsed(string orderDate, string batchNo)
        {
            string sql = string.Format("SELECT CHANNELCODE,ProductCODE,ProductNAME,SUM(QUANTITY) QUANTITY ,MAX(CHANNELORDER) CHANNELORDER," +
                                       "(SELECT COUNT(DISTINCT ProductCODE) FROM SC_CHANNELUSED WHERE CHANNELTYPE='3' AND ProductCODE != '') CIGARETTECOUNT " +
                                       " FROM SC_CHANNELUSED " +
                                       " WHERE ORDERDATE='{0}' AND BATCHNO='{1}' AND CHANNELTYPE='3' AND ProductCODE != '' " +
                                       " GROUP BY CHANNELCODE,ProductCODE,ProductNAME ORDER BY CHANNELORDER ", orderDate, batchNo);
                                       //" GROUP BY CHANNELCODE,ProductCode,ProductName ORDER BY SUM(QUANTITY) DESC,CHANNELORDER ", orderDate, batchNo);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 2010-11-21 todo
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet FindOrderMaster(string orderDate, string batchNo, string lineCode)
        {
            string sql = "SELECT A.ORDERDATE,A.BATCHNO, B.LINECODE, ROW_NUMBER() over (ORDER BY D.SORTID,A.ROUTECODE, E.SORTID) SORTNO, " +
                            " A.ORDERID, A.AREACODE, C.AREANAME, A.ROUTECODE, D.ROUTENAME, A.CUSTOMERCODE, E.CUSTOMERNAME,E.ADDRESS, E.LICENSENO, " +
                            " 0 TQUANTITY, 0 QUANTITY, 0 PQUANTITY, 0 PACKQUANTITY,E.SORTID ORDERNO, 1 EXPORTNO, 1 EXPORTNO1, '0', NULL " +
                            " FROM SC_I_ORDERMASTER A " +
                            " LEFT JOIN SC_LINE B ON A.ROUTECODE = B.ROUTECODE  AND A.ORDERDATE = B.ORDERDATE AND A.BATCHNO = B.BATCHNO " +
                            " LEFT JOIN CMD_AREA C ON A.AREACODE = C.AREACODE " +
                            " LEFT JOIN CMD_ROUTE D ON A.ROUTECODE = D.ROUTECODE " +
                            " LEFT JOIN CMD_CUSTOMER E ON A.CUSTOMERCODE = E.CUSTOMERCODE " +
                            " WHERE A.ORDERDATE = '{0}' AND A.BATCHNO = '{1}' AND B.LINECODE = '{2}' " +
                            " ORDER BY SORTNO";

            sql = "SELECT A.ORDERDATE,A.BATCHNO, B.LINECODE, " +
                //分拣流水号
                    " ROW_NUMBER() over (ORDER BY C.SORTID,D.SORTID,A.ROUTECODE, A.SORTID) SORTNO, " +

                    " A.ORDERID, A.AREACODE, C.AREANAME, A.ROUTECODE, D.ROUTENAME, A.CUSTOMERCODE, E.CUSTOMERNAME,E.ADDRESS, E.LICENSENO, " +
                    " 0 TQUANTITY, 0 QUANTITY, 0 PQUANTITY, 0 PACKQUANTITY, " +

                    //当前订单异形烟数量
                    " ISNULL((SELECT ISNULL(SUM(F.QUANTITY),0) FROM SC_I_ORDERDETAIL F " +
                        " LEFT JOIN CMD_Product G ON F.ProductCode = G.ProductCode " +
                        " WHERE G.ISABNORMITY = '1' AND F.ORDERID = A.ORDERID " +
                        " GROUP BY F.ORDERID),0) ABNORMITY_QUANTITY," +

                    //配送序号（业务配送序号）
                //" E.SORTID ORDERNO, " + 

                    //分拣订单在当前分拣线路中的分拣序号（根据业务配送序号分拣生成的连续顺序号）
                    " (SELECT ORDERNO FROM (SELECT A1.*,ROW_NUMBER() over (ORDER BY A1.SORTID) ORDERNO FROM SC_I_ORDERMASTER A1 " +
                        " LEFT JOIN CMD_CUSTOMER B1 ON A1.CUSTOMERCODE = B1.CUSTOMERCODE " +
                        " WHERE A1.ROUTECODE = A.ROUTECODE AND A1.ORDERDATE = '{0}' AND A1.BATCHNO = '{1}') TEMP WHERE TEMP.ORDERID = A.ORDERID) ORDERNO," +

                    //分拣订单在全部订单中的分拣序号（根据业务配送序号分拣生成的连续顺序号）
                    " ROW_NUMBER() over (ORDER BY  C.SORTID,D.SORTID,A.ROUTECODE, A.SORTID) CUSTOMERSORTNO," +

                    //包状机号
                    " 1 EXPORTNO, 1 EXPORTNO1, '0', NULL ," +
                    " 0 PACKNO ,0 PACKNO1,0 PACKORDERNO" +

                    " FROM SC_I_ORDERMASTER A " +
                    " LEFT JOIN SC_LINE B ON A.ROUTECODE = B.ROUTECODE  AND A.ORDERDATE = B.ORDERDATE AND A.BATCHNO = B.BATCHNO " +
                    " LEFT JOIN CMD_AREA C ON A.AREACODE = C.AREACODE " +
                    " LEFT JOIN CMD_ROUTE D ON A.ROUTECODE = D.ROUTECODE " +
                    " LEFT JOIN CMD_CUSTOMER E ON A.CUSTOMERCODE = E.CUSTOMERCODE " +

                    " LEFT JOIN (SELECT I.* FROM SC_I_ORDERDETAIL I " +
                                " LEFT JOIN CMD_Product J ON I.ProductCode = J.ProductCode WHERE J.ISABNORMITY NOT IN ('1')) K" +
                            " ON A.ORDERID = K.ORDERID" +
                //条件
                    " WHERE A.ORDERDATE = '{0}' AND A.BATCHNO = '{1}' AND B.LINECODE = '{2}' " +
                    " AND A.ORDERID NOT IN (SELECT ORDERID FROM SC_HANDLE_SORT_ORDER WHERE ORDERDATE = '{0}') " +
                //分组
                    " GROUP BY A.ORDERDATE,  A.BATCHNO, B.LINECODE,A.ORDERID, A.AREACODE,C.SORTID,C.AREANAME, A.ROUTECODE," +
                        " D.ROUTENAME, A.CUSTOMERCODE, E.CUSTOMERNAME,E.ADDRESS, E.LICENSENO,D.SORTID,A.ROUTECODE, A.SORTID " +
                //条件
                    " HAVING ISNULL(SUM(K.QUANTITY),0) > 0 " +
                //排序
                    " ORDER BY SORTNO";
            sql = string.Format(sql, orderDate, batchNo, lineCode);
            return ExecuteQuery(sql);
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet FindOrderDetail(string batchNo, string lineCode)
        {
            string sql = "SELECT A.*, B.CHANNELCODE " +
                            " FROM (SELECT * FROM SC_I_ORDERDETAIL " +
                                    " WHERE ProductCode NOT IN (SELECT ProductCode FROM CMD_Product WHERE ISABNORMITY = '1') " +
                                    " AND ORDERID IN (SELECT ORDERID FROM SC_I_ORDERMASTER " +
                                                      " WHERE BATCHNO = '{0}' " +
                                                      " AND ROUTECODE IN (SELECT ROUTECODE FROM SC_LINE " +
                                                                          " WHERE BATCHNO = '{0}' AND LINECODE = '{1}'))) A " +
                            " LEFT JOIN (SELECT ProductCode,MIN(CHANNELCODE) CHANNELCODE " +
                                        " FROM SC_CHANNELUSED " +
                                        " WHERE LINECODE='{1}' AND BATCHNO = '{0}' " +
                                        " GROUP BY ProductCode) B " +
                            " ON A.ProductCode = B.ProductCode " +
                            " WHERE A.ORDERID NOT IN (SELECT ORDERID FROM SC_HANDLE_SORT_ORDER)" +
                            " ORDER BY ORDERID,CHANNELCODE";
            sql = string.Format(sql, batchNo, batchNo, lineCode);
            return ExecuteQuery(sql);
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet FindOrderDetail(string orderDate, string batchNo, string lineCode)
        {
            string sql = "SELECT A.*, B.CHANNELCODE " +
                            " FROM (SELECT * FROM SC_I_ORDERDETAIL " +
                                    " WHERE PRODUCTCODE NOT IN (SELECT PRODUCTCODE FROM CMD_Product WHERE ISABNORMITY = '1') " +
                                    " AND ORDERID IN (SELECT ORDERID FROM SC_I_ORDERMASTER " +
                                                      " WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}' " +
                                                      " AND ROUTECODE IN (SELECT ROUTECODE FROM SC_LINE " +
                                                                          " WHERE ORDERDATE = '{2}' AND BATCHNO = '{3}' AND LINECODE = '{4}'))) A " +
                            " LEFT JOIN (SELECT PRODUCTCODE,MIN(CHANNELCODE) CHANNELCODE " +
                                        " FROM SC_CHANNELUSED " +
                                        " WHERE LINECODE='{4}' AND ORDERDATE = '{0}' AND BATCHNO = '{1}' " +
                                        " GROUP BY PRODUCTCODE) B " +
                            " ON A.PRODUCTCODE = B.PRODUCTCODE " +
                            " WHERE A.ORDERID NOT IN (SELECT ORDERID FROM SC_HANDLE_SORT_ORDER WHERE ORDERDATE = '{0}')" +
                            " ORDER BY ORDERID,CHANNELCODE";
            sql = string.Format(sql, orderDate, batchNo, orderDate, batchNo, lineCode);
            return ExecuteQuery(sql);
        }

        public DataTable FindOrderRoute(string orderDate, string batchNo)
        {
            string sql = string.Format("SELECT B.*,C.AREANAME " +
                " FROM (SELECT DISTINCT ROUTECODE FROM SC_I_ORDERMASTER WHERE ORDERDATE='{0}' AND BATCHNO='{1}') A " +
                " LEFT JOIN CMD_ROUTE B ON A.ROUTECODE=B.ROUTECODE " +
                " LEFT JOIN CMD_AREA C ON B.AREACODE=C.AREACODE " +
                " ORDER BY B.AREACODE,B.ROUTECODE", orderDate, batchNo);
            return ExecuteQuery(sql).Tables[0];
        }

        public void BatchInsertMaster(DataTable dtData)
        {
            BatchInsert(dtData, "SC_I_ORDERMASTER");
        }

        public void BatchInsertDetail(DataTable dtData)
        {
            BatchInsert(dtData, "SC_I_ORDERDETAIL");
        }
        public void BatchInsert(DataTable dtData)
        {
            ExecuteQuery("TRUNCATE TABLE SC_I_ORDER");
            BatchInsert(dtData, "SC_I_ORDER");
        }
        public void BatchInsertOrder(DataTable dtData, string BatchNo)
        {
            ExecuteQuery(string.Format("DELETE FROM SC_I_ORDER WHERE BATCH_NO='{0}'", BatchNo));
            BatchInsert(dtData, "SC_I_ORDER");
        }
        public void BatchInsertOrder(string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_I_ORDERMASTER WHERE BATCHNO='{0}';DELETE FROM SC_I_ORDERDETAIL WHERE BATCHNO='{0}'",batchNo);
            ExecuteQuery(sql);
            
            sql = "INSERT INTO SC_I_ORDERMASTER (ORDERDATE, DELIVERYDATE, BATCHNO, ORDERID, ROUTECODE, DEPTCODE, CUSTOMERCODE, CUSTOMERDESC, SORTID, DETAILNUM) " +
                  "SELECT ORDER_DATE,DELIVERY_DATE,BATCH_NO,ORDER_NO,ROUTE_CODE,DEPT_CODE,CUSTOMER_CODE ,CUSTOMER_DESC ,SEND_SEQ,COUNT(PRODUCT_CODE) " +
                  "FROM SC_I_ORDER " +
                  "WHERE SC_I_ORDER.BATCH_NO='{0}' " +
                  "GROUP BY ORDER_DATE ,DELIVERY_DATE,BATCH_NO ,ORDER_NO,ROUTE_CODE,DEPT_CODE,CUSTOMER_CODE,CUSTOMER_DESC,SEND_SEQ";
            sql = string.Format(sql, batchNo);
            ExecuteQuery(sql);

            sql = "INSERT INTO SC_I_ORDERDETAIL (ORDERDETAILID, ORDERID, ProductCode, ProductName, QUANTITY, ORDERDATE,DELIVERDATE,BATCHNO,ORDER_QUANTITY) " +
                  "SELECT ROW_NUMBER() OVER(PARTITION BY ORDER_NO ORDER BY ORDER_NO),ORDER_NO ,PRODUCT_CODE,PRODUCT_DESC,ORDER_QUANT,ORDER_DATE,DELIVERY_DATE,BATCH_NO,ORDER_QUANT " +
                  "FROM SC_I_ORDER " +
                  "WHERE SC_I_ORDER.BATCH_NO='{0}'";
            sql = string.Format(sql, batchNo);
            ExecuteQuery(sql);
        }
        public DataTable GetOrder()
        {
            string sql = string.Format("SELECT * FROM SC_I_ORDER");
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        public void DeleteHistory(string orderDate)
        {
            string sql = string.Format("DELETE FROM SC_I_ORDERDETAIL WHERE ORDERDATE < '{0}'", orderDate);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_I_ORDERMASTER WHERE ORDERDATE < '{0}'", orderDate);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteOrder(string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_ORDER_MASTER WHERE BATCHNO = '{0}'", batchNo);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_ORDER_DETAIL WHERE BATCHNO = '{0}'", batchNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteOrder(string orderDate, string batchNo)
        {
            //string sql = string.Format("DELETE FROM SC_I_ORDERDETAIL WHERE ORDERID IN (SELECT ORDERID FROM SC_I_ORDERMASTER WHERE ORDERDATE = '{0}' AND BATCHNO={1})", orderDate, batchNo);
            //ExecuteNonQuery(sql);

            //sql = string.Format("DELETE FROM SC_I_ORDERMASTER WHERE ORDERDATE = '{0}' AND BATCHNO = {1}", orderDate, batchNo);
            //ExecuteNonQuery(sql);

            string sql = string.Format("DELETE FROM SC_ORDER_MASTER WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_ORDER_DETAIL WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);
        }

        public void DeleteNoUseOrder(string orderDate, string batchNo, string routes)
        {
            string sql = "DELETE FROM SC_I_ORDERDETAIL WHERE ORDERDATE='{0}' AND BATCHNO={1} AND ORDERID NOT IN " +
                          "(SELECT ORDERID FROM SC_I_ORDERMASTER WHERE ORDERDATE='{0}' AND BATCHNO='{1}' AND ROUTECODE IN ({2}))";
            ExecuteNonQuery(string.Format(sql, orderDate, batchNo, routes));
            sql = "DELETE FROM SC_I_ORDERMASTER WHERE ORDERDATE='{0}' AND BATCHNO='{1}' AND ROUTECODE NOT IN ({2})";
            ExecuteNonQuery(string.Format(sql, orderDate, batchNo, routes));
        }
        public DataTable FindTmpMaster(string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT * FROM SC_TMP_PALLETMASTER WHERE BATCHNO = '{0}' AND LINECODE = '{1}'", batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindTmpMaster(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT * FROM SC_ORDER_MASTER WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}' AND LINECODE = '{2}'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindTmpDetail(string orderDate, string batchNo, string lineCode)
        {
            string sql = string.Format("SELECT * FROM SC_ORDER_DETAIL WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}' AND LINECODE = '{2}'", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }
        public List<int> FindRouteMaxPackNoList()
        {
            List<int> routeMaxPackNoList = new List<int>();
            string sql = @"SELECT ISNULL(MAX(PACKNO),0) AS ROUTE_MAX_PACKNO,ISNULL(MAX(PACKNO1),0) AS ROUTE_MAX_PACKNO1 
                            FROM SC_PALLETMASTER  
                            GROUP BY ROUTECODE";
            DataTable table = ExecuteQuery(sql).Tables[0];
            foreach (DataRow row in table.Rows)
            {
                int maxPackNo = Convert.ToInt32(row["ROUTE_MAX_PACKNO"]) > Convert.ToInt32(row["ROUTE_MAX_PACKNO1"]) ? Convert.ToInt32(row["ROUTE_MAX_PACKNO"]) : Convert.ToInt32(row["ROUTE_MAX_PACKNO1"]);
                routeMaxPackNoList.Add(maxPackNo);
            }
            return routeMaxPackNoList;
        }

        public DataTable FindMaster()
        {
            string sql = "SELECT ORDERDATE,BATCHNO,SORTNO, ORDERID,ROUTECODE,ROUTENAME,CUSTOMERCODE,CUSTOMERNAME, " +
                         "CASE STATUS WHEN '0' THEN '未下单' ELSE '已下单' END STATUS,QUANTITY,CASE STATUS1 WHEN '0' THEN '未下单' ELSE '已下单' END STATUS1,QUANTITY1," +
                         "CASE WHEN PACKQUANTITY=QUANTITY THEN '已发送' ELSE '未发送' END PACKAGE," +
                         "CASE WHEN PACKQUANTITY1=QUANTITY1 THEN '已发送' ELSE '未发送' END PACKAGE1, " +
                         "CASE WHEN (FINISHEDTIME IS NOT NULL OR QUANTITY=0) AND (FINISHEDTIME1 IS NOT NULL OR QUANTITY1=0) THEN '完成' ELSE '未完成' END STATUS2 " +
                         "FROM SC_ORDER_MASTER ORDER BY SORTNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindDetail(string sortNo)
        {
            string sql = string.Format("SELECT A.SORTNO, A.PACKNO,ORDERID, B.CHANNELNAME, "+
                                            " CASE B.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                                            " A.ProductCode, A.ProductName, A.QUANTITY ," +
                                            " CASE WHEN A.CHANNELGROUP=1 THEN 'A线' ELSE 'B线' END  CHANNELLINE "+
                                            " FROM SC_ORDER_DETAIL A " +
                                            " LEFT JOIN SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                                            " WHERE A.SORTNO='{0}' ORDER BY A.CHANNELGROUP,B.CHANNELADDRESS DESC", sortNo);
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable GetOrderDetail()
        {
            string sql = string.Format("SELECT *, B.CHANNELNAME " +
                                       "FROM SC_ORDER_DETAIL A " +
                                       "LEFT JOIN SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE AND A.ORDERDATE=B.ORDERDATE " + 
                                       "LEFT JOIN SC_CHANNELBALANCE C ON A.CHANNELCODE=C.CHANNELCODE AND A.ORDERDATE=C.ORDERDATE " + 
                                       "ORDER BY A.SORTNO,A.CHANNELCODE ");
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable GetOrderDetail(string OrderId)
        {
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY B.CHANNELTYPE DESC,B.CHANNELADDRESS,A.PRODUCTCODE) ROWID,A.SORTNO, A.PACKNO,ORDERID, B.CHANNELNAME, " +
                                            " CASE B.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                                            " A.PRODUCTCODE, A.PRODUCTNAME, A.QUANTITY ,(CASE E.ISABNORMITY WHEN 0 THEN '否' ELSE '是' END) ISABNORMITY, " +
                                            " CASE WHEN A.CHANNELGROUP=1 THEN 'A线' ELSE 'B线' END  CHANNELLINE " +
                                            " FROM SC_ORDER_DETAIL A " +
                                            " LEFT JOIN SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE AND A.BATCHNO=B.BATCHNO " +
                                            " LEFT JOIN CMD_Product E ON A.PRODUCTCODE=E.PRODUCTCODE " +
                                            " WHERE A.ORDERID='{0}' ORDER BY B.CHANNELTYPE DESC,B.CHANNELADDRESS,A.PRODUCTCODE ", OrderId);
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindCigarettes()
        {
            string sql = "SELECT ProductCode,ProductName,SUM(QUANTITY) FROM SC_ORDER_DETAIL GROUP BY ProductCode,ProductName ORDER BY ProductCode";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrderWithCigarette(string ProductCode)
        {
            string sql = "SELECT MIN(A.SORTNO) SORTNO,A.ORDERDATE,A.BATCHNO,A.LINECODE,A.ORDERID,B.ROUTENAME,B.CUSTOMERNAME,A.ProductCode,A.ProductName,SUM(A.QUANTITY) QUANTITY,CHANNELCODE" +
                         " FROM SC_ORDER A" +
                         " LEFT JOIN SC_PALLETMASTER B ON A.ORDERID = B.ORDERID"+
                         " WHERE ProductCode = '{0}'" +
                         " GROUP BY A.ORDERDATE,A.BATCHNO,A.LINECODE,A.ORDERID,B.ORDERID,A.ProductCode,A.ProductName,A.CHANNELCODE,B.ROUTENAME,B.CUSTOMERNAME" +
                         " ORDER BY A.ORDERDATE,A.BATCHNO,A.LINECODE,MIN(A.SORTNO)";
            sql = string.Format(sql,ProductCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindPackMaster()
        {
            string sql = "SELECT ROW_NUMBER() OVER(ORDER BY MIN(SORTNO)) AS PACKNO,MIN(SORTNO) AS SORTNO ,ORDERDATE,ORDERID,ROUTECODE,ROUTENAME,CUSTOMERCODE,CUSTOMERNAME,SUM(QUANTITY) AS QUANTITY, SUM(QUANTITY1) AS QUANTITY1 " +
                            "FROM SC_ORDER_MASTER GROUP BY ORDERDATE,ROUTECODE,ROUTENAME,ORDERID,CUSTOMERCODE,CUSTOMERNAME ORDER BY SORTNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindPackMaster(string [] filter)
        {
            string sql = "SELECT B.* FROM " +
                            " (" +
	                            " SELECT ROW_NUMBER() OVER(ORDER BY MIN(A.SORTNO)) AS PACKNO," +
	                            " MIN(A.SORTNO) AS SORTNO ,A.ORDERDATE,A.ORDERID,A.ROUTECODE,A.ROUTENAME," +
	                            " A.CUSTOMERCODE,A.CUSTOMERNAME,SUM(A.QUANTITY) AS QUANTITY" +
                                " FROM SC_ORDER_MASTER A" +
	                            " GROUP BY A.ORDERDATE,A.ROUTECODE,A.ROUTENAME,A.ORDERID,A.CUSTOMERCODE,A.CUSTOMERNAME " +
                            " ) B " +
                            " LEFT JOIN SC_ORDER_DETAIL C ON B.ORDERID = C.ORDERID " +
                            " WHERE {0} " +
                            " GROUP BY B.PACKNO,B.SORTNO,B.ORDERDATE,B.ROUTECODE,B.ROUTENAME,B.ORDERID,B.CUSTOMERCODE,B.CUSTOMERNAME,B.QUANTITY" +
                            " {1} " +
                            " ORDER BY SORTNO";
            return ExecuteQuery(string.Format(sql,filter)).Tables[0];
        }


        public DataTable FindPackDetail()
        {
            //string sql = string.Format("SELECT A.ORDERID, A.ProductCode,A.ProductName, SUM(A.QUANTITY) QUANTITY ,CASE WHEN A.CHANNELGROUP=1 THEN 'A线' ELSE 'B线' END  CHANNELLINE,A.CHANNELGROUP " +
            //                        "FROM SC_ORDER A  LEFT JOIN DBO.SC_CHANNELUSED B ON A.CHANNELCODE = B.CHANNELCODE WHERE A.ORDERID='{0}' "+
            //                        " GROUP BY ORDERID,A.CHANNELGROUP,A.SORTNO ,B.CHANNELNAME,A.ProductCode,A.ProductName ORDER BY A.CHANNELGROUP,A.SORTNO,B.CHANNELNAME", orderId);
            //return ExecuteQuery(sql).Tables[0];
            string sql = string.Format("SELECT A.PACKNO AS PACK,A.SORTNO, ORDERID, B.CHANNELNAME, " +
                                " CASE B.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                                " A.ProductCode, A.ProductName, A.QUANTITY ," +
                                " CASE WHEN A.CHANNELGROUP=1 THEN 'A线' ELSE 'B线' END  CHANNELLINE " +
                                " FROM SC_ORDER_DETAIL A " +
                                " LEFT JOIN SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                                " ORDER BY A.CHANNELGROUP,A.SORTNO DESC,B.CHANNELADDRESS DESC");
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindMergeSortMaster()
        {
            string sql = "SELECT TOP 1 * FROM SC_ORDER_MASTER WHERE MERGESTATUS=0 ORDER BY SORTNO";
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindMergeSortMaster(int sortNo)
        {
            string sql = string.Format("SELECT TOP 1 * FROM SC_ORDER_MASTER WHERE SORTNO={0}",sortNo);
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindSortMaster()
        {
            string sql = "SELECT TOP 1 * FROM SC_ORDER_MASTER WHERE STATUS=0 ORDER BY SORTNO";
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindSortMaster(string channelType)
        {
            string sql = string.Format("SELECT TOP 1 * FROM SC_ORDER_MASTER WHERE STATUS{0}=0 AND QUANTITY{0}>0 ORDER BY SORTNO", channelType == "3" ? "" : "1");
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindSortMaster(string channelType,string requestNo)
        {
            string sql = string.Format("SELECT TOP 1 * FROM SC_ORDER_MASTER WHERE STATUS{0}=0 AND SORTNO %4={1} ORDER BY SORTNO", channelType == "3" ? "" : "1", requestNo);
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindSortSpeed()
        {
            string sql = "SELECT * FROM 效率报表";
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据订单ID和A线或者B线查询换户的流水号
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个换户的流水号</returns>
        public string FindMaxSortNoFromMasterByOrderID(string orderID,string channelType)
        {
            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER_MASTER WHERE ORDERID='{0}' AND QUANTITY != 0 ";
            return ExecuteScalar(string.Format(sql, orderID)).ToString();
        }
        /// <summary>
        /// 根据订单ID和A线或者B线查询换户的流水号
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个换户的流水号</returns>
        public string FindMaxSortNo()
        {
            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER_MASTER ";
            return ExecuteScalar(sql).ToString();
        }
        /// <summary>
        /// 根据订单ID和A线或者B线查询换户的流水号
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个换户的流水号</returns>
        public string FindMaxSortNo(string channelType)
        {
            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER_MASTER WHERE QUANTITY{0} != 0 ";
            return ExecuteScalar(string.Format(sql, channelType=="3"?"":"1")).ToString();
        }
        /// <summary>
        /// 查询本分拣线组是否结束
        /// </summary>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个流水号</returns>
        public string FindEndSortNoForChannelGroup(string channelGroup)
        {
            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM SC_PALLETMASTER WHERE QUANTITY{0} != 0 ";
            return ExecuteScalar(string.Format(sql, channelGroup == "A" ? "" : "1")).ToString();
        }
        /// <summary>
        /// 根据A线或B线查询当前缺烟的流水号
        /// </summary>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个流水号</returns>
        public string FindMaxSortedMaster()
        {
            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER_MASTER WHERE FINISHEDTIME IS NOT NULL AND FINISHEDTIME1 IS NOT NULL AND STATUS ='1' AND STATUS1='1'";
            return ExecuteScalar(sql).ToString();
        }
        /// <summary>
        /// 根据A线或B线查询当前缺烟的流水号
        /// </summary>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个流水号</returns>
        public string FindMaxSortedMaster(string channelType)
        {
            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER_MASTER WHERE STATUS{0} ='1'";
            return ExecuteScalar(string.Format(sql, channelType == "3" ? "" : "1")).ToString();
        }
        /// <summary>
        /// 根据当前要分拣的流水号查询数据
        /// </summary>
        /// <param name="sortNo">当前要分拣的流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个表的数据</returns>
        public DataTable FindSortDetail(string sortNo)
        {
            string sql = "SELECT A.CHANNELADDRESS,A.CHANNELCODE, A.CHANNELTYPE, ISNULL(B.QUANTITY,0) QUANTITY" +
                " FROM SC_CHANNELUSED A " +
                " LEFT JOIN (SELECT SORTNO,CHANNELCODE,SUM(QUANTITY) QUANTITY FROM SC_ORDER_DETAIL GROUP BY SORTNO,CHANNELCODE) B " +
                        " ON A.CHANNELCODE = B.CHANNELCODE AND B.SORTNO = '{0}' " +
                " ORDER BY A.CHANNELADDRESS, A.CHANNELTYPE , A.CHANNELCODE";
            return ExecuteQuery(string.Format(sql, sortNo)).Tables[0];
        }
        /// <summary>
        /// 根据当前要分拣的流水号查询数据
        /// </summary>
        /// <param name="sortNo">当前要分拣的流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个表的数据</returns>
        public DataTable FindSortDetail(string sortNo,string channelType)
        {
            string sql = "SELECT A.CHANNELADDRESS,A.CHANNELCODE, A.CHANNELTYPE, ISNULL(B.QUANTITY,0) QUANTITY,ISNULL(C.QUANTITY,0) ORDERQUANTITY,D.SORTNO MAXSORTNO" +
                " FROM SC_CHANNELUSED A " +
                " INNER JOIN (SELECT BATCHNO,SORTNO,CHANNELCODE,SUM(QUANTITY) QUANTITY FROM SC_ORDER_DETAIL GROUP BY BATCHNO,SORTNO,CHANNELCODE) B " +
                        " ON A.BATCHNO=B.BATCHNO AND A.CHANNELCODE = B.CHANNELCODE AND B.SORTNO = '{0}' " +
                " INNER JOIN (SELECT BATCHNO,SORTNO,SUM(QUANTITY) QUANTITY FROM SC_ORDER_DETAIL GROUP BY BATCHNO,SORTNO) C " +
                        " ON A.BATCHNO=C.BATCHNO AND C.SORTNO = '{0}' " +
                " INNER JOIN (SELECT BATCHNO,MAX(SORTNO) FROM SC_ORDER_DETAIL GROUP BY BATCHNO) D " +
                        " ON A.BATCHNO=D.BATCHNO " +
                " INNER JOIN (SELECT TOP 1 CHANNELCODE FROM SC_ORDER_DETAIL WHERE QUANTITY>0 ORDER BY SORTNO DESC,CHANNELCODE DESC) D " +
                        " ON A.BATCHNO=D.BATCHNO " +
                "WHERE A.CHANNELTYPE='{1}' " +
                " ORDER BY A.CHANNELADDRESS, A.CHANNELTYPE , A.CHANNELCODE";
            return ExecuteQuery(string.Format(sql, sortNo, channelType)).Tables[0];
        }
        /// <summary>
        /// 根据当前要分拣的流水号查询数据
        /// </summary>
        /// <param name="sortNo">当前要分拣的流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个表的数据</returns>
        public DataTable FindSortDetail(string sortNo, string channelType,int GroupNo)
        {
            string sql = "SELECT A.CHANNELADDRESS,A.CHANNELCODE, A.CHANNELTYPE, ISNULL(B.QUANTITY,0) QUANTITY,ISNULL(C.QUANTITY,0) ORDERQUANTITY,D.SORTNO MAXSORTNO," +
                         "(SELECT TOP 1 CHANNELADDRESS FROM SC_ORDER_DETAIL INNER JOIN SC_CHANNELUSED ON SC_ORDER_DETAIL.BATCHNO=SC_CHANNELUSED.BATCHNO AND SC_ORDER_DETAIL.CHANNELCODE=SC_CHANNELUSED.CHANNELCODE " +
                         "WHERE SC_ORDER_DETAIL.QUANTITY>0 AND SC_CHANNELUSED.GROUPNO={2} ORDER BY SC_ORDER_DETAIL.SORTNO DESC,SC_ORDER_DETAIL.CHANNELCODE DESC) LASTCHANNELADDRESS" +
                " FROM SC_CHANNELUSED A " +
                " INNER JOIN (SELECT BATCHNO,SORTNO,CHANNELCODE,SUM(QUANTITY) QUANTITY FROM SC_ORDER_DETAIL GROUP BY BATCHNO,SORTNO,CHANNELCODE) B " +
                        " ON A.BATCHNO=B.BATCHNO AND A.CHANNELCODE = B.CHANNELCODE AND B.SORTNO = '{0}' " +
                " INNER JOIN (SELECT BATCHNO,SORTNO,SUM(QUANTITY) QUANTITY FROM SC_ORDER_DETAIL GROUP BY BATCHNO,SORTNO) C " +
                        " ON A.BATCHNO=C.BATCHNO AND C.SORTNO = '{0}' " +
                " INNER JOIN (SELECT BATCHNO,MAX(SORTNO) SORTNO FROM SC_ORDER_DETAIL GROUP BY BATCHNO) D " +
                        " ON A.BATCHNO=D.BATCHNO " +                
                "WHERE A.CHANNELTYPE='{1}' AND A.GROUPNO={2}" +
                " ORDER BY A.CHANNELADDRESS, A.CHANNELTYPE , A.CHANNELCODE";
            return ExecuteQuery(string.Format(sql, sortNo, channelType,GroupNo)).Tables[0];
        }
        /// <summary>
        /// 根据当前要分拣的流水号查询数据
        /// </summary>
        /// <param name="sortNo">当前要分拣的流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个表的数据</returns>
        //public DataTable FindSortDetail(string sortNo, string channelGroup)
        //{
        //    string sql = "SELECT A.CHANNELADDRESS,A.CHANNELCODE, A.CHANNELTYPE, ISNULL(B.QUANTITY,0) QUANTITY" +
        //        " FROM SC_CHANNELUSED A "+
        //        " LEFT JOIN (SELECT SORTNO,CHANNELCODE,SUM(QUANTITY) QUANTITY FROM SC_ORDER GROUP BY SORTNO,CHANNELCODE) B "+
        //                " ON A.CHANNELCODE = B.CHANNELCODE AND B.SORTNO = '{0}' "+
        //        " WHERE A.CHANNELGROUP = {1} " + 
        //        " ORDER BY A.CHANNELADDRESS, A.CHANNELTYPE , A.CHANNELCODE";
        //    return ExecuteQuery(string.Format(sql, sortNo,channelGroup == "A" ? "1" : "2")).Tables[0];
        //}

        public DataTable FindDispatchingMasterPack(string sortNo)
        { 
            string sql =string.Format( "SELECT * FROM SC_PALLETMASTER WHERE SORTNO ='{0}' ",sortNo);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindMasterInfo(string sortNo)
        {
            string sql = string.Format("SELECT ROUTENAME, CUSTOMERNAME FROM SC_PALLETMASTER WHERE SORTNO={0}", sortNo);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindCigaretteDetail(string sortNo)
        {
            string sql = string.Format("SELECT ProductName, SUM(QUANTITY) QUANTITY FROM SC_ORDER WHERE SORTNO={0} " +
                "GROUP BY ProductName ORDER BY SUM(QUANTITY) DESC", sortNo);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 根据流水号查询分拣完成的数量等数据
        /// </summary>
        /// <param name="sortNo">完成的流水号</param>
        /// <returns>返回查询到的数据表</returns>
        public DataTable FindOrderInfo(string sortNo)
        {
            string sql = "";
            if (sortNo != null)
                //sql = "SELECT COUNT(DISTINCT CUSTOMERCODE) CUSTOMERNUM, COUNT(DISTINCT ROUTECODE) ROUTENUM, " +
                //         " (SELECT ISNULL(SUM(QUANTITY),0) FROM SC_ORDER_MASTER WHERE FINISHEDTIME <= GETDATE() AND STATUS=1 ) QUANTITY, " +
                //         " (SELECT ISNULL(SUM(QUANTITY1),0) FROM SC_ORDER_MASTER WHERE FINISHEDTIME1 <= GETDATE() AND STATUS1=1 ) QUANTITY1 " +
                //         " FROM SC_ORDER_MASTER WHERE FINISHEDTIME <= GETDATE() OR FINISHEDTIME1 <= GETDATE()";
                
            sql = "SELECT COUNT(DISTINCT CUSTOMERCODE) CUSTOMERNUM, COUNT(DISTINCT ROUTECODE) ROUTENUM, " +
                         " (SELECT ISNULL(SUM(QUANTITY),0) FROM SC_ORDER_MASTER WHERE FINISHEDTIME <= GETDATE() AND STATUS=1 ) QUANTITY, " +
                         " (SELECT ISNULL(SUM(QUANTITY1),0) FROM SC_ORDER_MASTER WHERE FINISHEDTIME1 <= GETDATE() AND STATUS1=1 ) QUANTITY1 " +
                         " FROM SC_ORDER_MASTER WHERE FINISHEDTIME IS NOT NULL AND FINISHEDTIME1 IS NOT NULL";
            else
                sql = "SELECT BATCHNO,COUNT(DISTINCT CUSTOMERCODE) CUSTOMERNUM, COUNT(DISTINCT ROUTECODE) ROUTENUM, " +
                        " ISNULL(SUM(QUANTITY),0) QUANTITY, ISNULL(SUM(QUANTITY1),0) QUANTITY1 " +
                        " FROM SC_ORDER_MASTER GROUP BY BATCHNO";
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 根据流水号查询分拣完成的数量等数据
        /// </summary>
        /// <param name="sortNo">完成的流水号</param>
        /// <returns>返回查询到的数据表</returns>
        public DataTable FindOrderInfo(string batchNo,string sortNo)
        {
            string sql = "";
            if (sortNo != null)
                sql = "SELECT COUNT(DISTINCT CUSTOMERCODE) CUSTOMERNUM, COUNT(DISTINCT ROUTECODE) ROUTENUM, " +
                      "(SELECT ISNULL(SUM(QUANTITY),0) FROM SC_ORDER_MASTER WHERE BATCHNO=A.BATCHNO AND FINISHEDTIME <= GETDATE() AND STATUS=1) QUANTITY, " +
                      "(SELECT ISNULL(SUM(QUANTITY1),0) FROM SC_ORDER_MASTER WHERE BATCHNO=A.BATCHNO AND FINISHEDTIME1 <= GETDATE() AND STATUS1=1) QUANTITY1 " +
                      "FROM SC_ORDER_MASTER A " +
                      "WHERE A.BATCHNO='{0}' AND (A.FINISHEDTIME <= GETDATE() OR A.FINISHEDTIME1 <= GETDATE())";
            else
                sql = "SELECT COUNT(DISTINCT CUSTOMERCODE) CUSTOMERNUM, COUNT(DISTINCT ROUTECODE) ROUTENUM, " +
                      "ISNULL(SUM(QUANTITY),0) QUANTITY, ISNULL(SUM(QUANTITY1),0) QUANTITY1 " +
                      "FROM SC_ORDER_MASTER " +
                      "WHERE BATCHNO='{0}'";
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 查询A线或B线已经完成的流水号
        /// </summary>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个流水号</returns>
        public int SortNoFinished(string sortNo)
        {
            string sql = string.Format("SELECT 1 FROM SC_ORDER_MASTER WHERE SORTNO={0} AND FINISHEDTIME IS NOT NULL AND FINISHEDTIME1 IS NOT NULL AND ISUPLOAD='1'",sortNo);
            object rows = ExecuteScalar(sql);
            if (rows != null)
                return int.Parse(rows.ToString());
            else
                return 0;
        }
        /// <summary>
        /// 查询A线或B线已经完成的流水号
        /// </summary>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个流水号</returns>
        public string FindLastSortNo()
        {
            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER_MASTER ";
            return ExecuteScalar(sql).ToString();
        }
        /// <summary>
        /// 查询A线或B线已经完成的流水号
        /// </summary>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个流水号</returns>
        public string FindLastSortNo(string channelType)
        {
            string sql = "SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER_MASTER WHERE STATUS{0}='1'";
            return ExecuteScalar(string.Format(sql, channelType == "3" ? "" : "1")).ToString();
        }

        /// <summary>
        /// 查询A线或B线最后客户的最小流水号
        /// </summary>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回一个流水号</returns>
        public string FindLastCustomerSortNo(string channelGroup)
        {
            string sql = "SELECT MIN(SORTNO) FROM SC_ORDER_MASTER WHERE CUSTOMERSORTNO = (SELECT MAX(CUSTOMERSORTNO) FROM SC_ORDER_MASTER) AND STATUS{0}=1";
            return ExecuteScalar(string.Format(sql, channelGroup == "A" ? "" : "1")).ToString();
        }


        /// <summary>
        /// 修改A线或者B线已分拣完的流水号的时间
        /// </summary>
        /// <param name="sortNo">完成的流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        public void UpdateFinishTime(string sortNo, string channelType)
        {
            string sql = "UPDATE SC_PALLETMASTER SET FINISHEDTIME{0} = GETDATE() WHERE STATUS{0}='1' AND SORTNO <= '{1}' ";
            sql = "UPDATE SC_ORDER_MASTER SET FINISHEDTIME{0} = GETDATE() WHERE STATUS{0}='1' AND SORTNO <= '{1}' AND FINISHEDTIME{0} IS NULL";
            ExecuteNonQuery(string.Format(sql, channelType == "3" ? "" : "1", sortNo));

            //最后一个订单号
            if (sortNo == "1")
            {
                sql = "UPDATE CMD_BATCH SET ENDSORTTIME=TMP.FINISHEDTIME FROM CMD_BATCH B INNER JOIN (SELECT BATCHNO,FINISHEDTIME FROM SC_ORDER_MASTER WHERE SORTNO=1) TMP ON B.BATCHNO=TMP.BATCHNO";
                ExecuteNonQuery(sql);
            }
        }
        /// <summary>
        /// 修改A线或者B线已分拣完的流水号的时间
        /// </summary>
        /// <param name="sortNo">完成的流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        public void UpdateFinishTime(string sortNo, string channelType,string maxSortNo)
        {
            string sql = "UPDATE SC_PALLETMASTER SET FINISHEDTIME{0} = GETDATE() WHERE STATUS{0}='1' AND SORTNO <= '{1}' ";
            sql = "UPDATE SC_ORDER_MASTER SET FINISHEDTIME{0} = GETDATE() WHERE STATUS{0}='1' AND SORTNO <= '{1}' AND FINISHEDTIME{0} IS NULL";
            ExecuteNonQuery(string.Format(sql, channelType == "3" ? "" : "1", sortNo));

            //最后一个订单号
            if (sortNo == maxSortNo)
            {
                sql = string.Format("UPDATE CMD_BATCH SET ENDSORTTIME=TMP.FINISHEDTIME FROM CMD_BATCH B INNER JOIN (SELECT BATCHNO,FINISHEDTIME FROM SC_ORDER_MASTER WHERE SORTNO={0}) TMP ON B.BATCHNO=TMP.BATCHNO",sortNo);
                ExecuteNonQuery(sql);
            }

            //2015/12/08修改 为了让第一个订单的完成时间作为批次开始分拣的时间
            //最后一个订单号
            if (sortNo == "1")
            {
                sql = string.Format("UPDATE CMD_BATCH SET BEGINSORTTIME=TMP.FINISHEDTIME FROM CMD_BATCH B INNER JOIN (SELECT BATCHNO,FINISHEDTIME FROM SC_ORDER_MASTER WHERE SORTNO={0}) TMP ON B.BATCHNO=TMP.BATCHNO", sortNo);
                ExecuteNonQuery(sql);
            }
        }
        /// <summary>
        /// 修改A线或者B线已分拣完流水号的时间
        /// </summary>
        /// <param name="sortNo">最后客户的最小流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        public void UpdateAllFinishTime(string sortNo, string channelType)
        {
            string sql = "UPDATE SC_ORDER_MASTER SET FINISHEDTIME{0} = GETDATE() WHERE STATUS{0}='1' AND SORTNO >= '{1}' ";
            ExecuteNonQuery(string.Format(sql, channelType == "3" ? "" : "1", sortNo));
        }

        public DataTable FindPackInfo()
        {
            //string sql = "SELECT TOP 1 SORTNO, (CEILING(QUANTITY/25.0)-CEILING((QUANTITY-PACKQUANTITY)/25.0)+1)  " +
            //    "AS BAGSN,CASE WHEN QUANTITY-PACKQUANTITY>=30 THEN 25 WHEN QUANTITY-PACKQUANTITY>25 THEN 20 ELSE QUANTITY-PACKQUANTITY " +
            //    "END AS QUANTITY FROM SC_PALLETMASTER WHERE QUANTITY-PACKQUANTITY>0 ORDER BY SORTNO";
            string sql = "SELECT TOP 400 MIN(SORTNO) SORTNO, ORDERID,SUM(QUANTITY) QUANTITY FROM SC_PALLETMASTER WHERE QUANTITY-PACKQUANTITY>0 GROUP BY ORDERID ORDER BY SORTNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdatePackQuantity(string sortNo, int quantity)
        {
            string sql = string.Format("UPDATE SC_PALLETMASTER SET PACKQUANTITY = {0} WHERE SORTNO = {1}", quantity, sortNo);
            ExecuteNonQuery(sql);
        }

        public void UpdatePackQuantityByOrderID(string orderID)
        {
            string sql = string.Format("UPDATE SC_PALLETMASTER SET PACKQUANTITY = QUANTITY WHERE ORDERID = '{0}'", orderID);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 把发送的流水号状态修改为已发送
        /// </summary>
        /// <param name="sortNo">已发送的流水号</param>
        /// <param name="status">状态</param>
        /// <param name="channelGroup">A线或B线</param>
        public void UpdateMergeOrderStatus(string sortNo, string status)
        {
            string sql = "UPDATE SC_ORDER_MASTER SET MERGESTATUS = '{0}' WHERE SORTNO = {1}";
            sql = string.Format(sql, status, sortNo);

            ExecuteNonQuery(sql);
            
        }
        /// <summary>
        /// 把发送的流水号状态修改为已发送
        /// </summary>
        /// <param name="sortNo">已发送的流水号</param>
        /// <param name="status">状态</param>
        /// <param name="channelGroup">A线或B线</param>
        public void UpdateBreakMergeOrderStatus(string sortNo)
        {
            string sql = "UPDATE SC_ORDER_MASTER SET MERGESTATUS = 0 WHERE SORTNO > {0};UPDATE SC_ORDER_MASTER SET MERGESTATUS = 1 WHERE SORTNO <= {0}";
            sql = string.Format(sql, sortNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 把发送的流水号状态修改为已发送
        /// </summary>
        /// <param name="sortNo">已发送的流水号</param>
        /// <param name="status">状态</param>
        /// <param name="channelGroup">A线或B线</param>
        public void UpdateOrderStatus(string sortNo, string status, string channelType)
        {
            string sql = "";
            if (status == "1")
            {
                //更新立式机或通道机订单为0的状态
                sql = "UPDATE SC_ORDER_MASTER SET STATUS{0} = '{1}',STARTTIME{0} = GETDATE(),FINISHEDTIME{0}=GETDATE() WHERE SORTNO < {2} AND QUANTITY{0}=0";
                ExecuteNonQuery(string.Format(sql, channelType == "3" ? "" : "1", status, sortNo));
                sql = "UPDATE SC_ORDER_MASTER SET STATUS{0} = '{1}',STARTTIME{0} = GETDATE() WHERE SORTNO = {2}";
            }
            else
            {
                sql = "UPDATE SC_ORDER_MASTER SET STATUS{0} = '{1}',STARTTIME{0} = NULL WHERE SORTNO = {2}";
            }
            ExecuteNonQuery(string.Format(sql, channelType == "3" ? "" : "1", status, sortNo));

            if (sortNo == "1")
            {
                sql = "UPDATE CMD_BATCH SET BEGINSORTTIME=TMP.STARTTIME FROM CMD_BATCH B INNER JOIN (SELECT BATCHNO,STARTTIME FROM SC_ORDER_MASTER WHERE SORTNO=1) TMP ON B.BATCHNO=TMP.BATCHNO";
                ExecuteNonQuery(sql);
            }
        }
        /// <summary>
        /// 把发送的流水号状态修改为已发送
        /// </summary>
        /// <param name="sortNo">已发送的流水号</param>
        /// <param name="status">状态</param>
        /// <param name="channelGroup">A线或B线</param>
        public void UpdateOrderStatus(string channelType)
        {
            //更新立式机或通道机订单为0的状态
            string sql = "UPDATE SC_ORDER_MASTER SET STATUS{0} = '1',STARTTIME{0} = GETDATE(),FINISHEDTIME{0}=GETDATE() WHERE STATUS{0}='0' AND QUANTITY{0}=0";
            ExecuteNonQuery(string.Format(sql, channelType == "3" ? "" : "1"));
        }
        public void UpdateChannel(string sourceChannel, string targetChannel)
        {
            string sql = string.Format("UPDATE SC_ORDER_DETAIL SET CHANNELCODE='{0}' WHERE CHANNELCODE='{1}'", targetChannel, sourceChannel);
            ExecuteNonQuery(sql);
        }
        public void UpdateChannel(string batchNo,string sourceChannel, string targetChannel)
        {
            string sql = string.Format("UPDATE SC_ORDER_DETAIL SET CHANNELCODE='{0}' WHERE CHANNELCODE='{1}' AND BATCHNO='{2}'", targetChannel, sourceChannel, batchNo);
            ExecuteNonQuery(sql);
            //sql = string.Format("UPDATE SC_BALANCE SET CHANNELCODE='{0}' WHERE CHANNELCODE='{1}' AND BATCHNO='{2}'", targetChannel, sourceChannel, batchNo);
            //ExecuteNonQuery(sql);
            //sql = string.Format("UPDATE SC_CHANNELBALANCE SET CHANNELCODE='{0}' WHERE CHANNELCODE='{1}' AND BATCHNO='{2}'", targetChannel, sourceChannel, batchNo);
            //ExecuteNonQuery(sql);
        }
        public void UpdateChannelBalance(string batchNo, string sourceChannel, string targetChannel, string targetChannelName, int targetChannelOrder)
        {
            string sql = string.Format("UPDATE SC_BALANCE SET CHANNELCODE='{0}',CHANNELNAME='{1}',CHANNELORDER={2} WHERE CHANNELCODE='{3}' AND BATCHNO='{4}'", targetChannel, targetChannelName, targetChannelOrder,sourceChannel, batchNo);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE SC_CHANNELBALANCE SET CHANNELCODE='{0}',CHANNELNAME='{1}' WHERE CHANNELCODE='{2}'", targetChannel, targetChannelName,sourceChannel);
            ExecuteNonQuery(sql);
        }
        //清除选中的那行之后的为未包装
        public void ClearPackQuantity(string orderID)
        {
            string sql = string.Format("UPDATE SC_PALLETMASTER SET PACKQUANTITY=0,PACKQUANTITY1=0 WHERE SORTNO>= (SELECT MIN(SORTNO) FROM SC_PALLETMASTER WHERE ORDERID = '{0}' )", orderID);
            ExecuteNonQuery(sql);
        }
        //标识选中的那行之前的为已包装
        public void UpdatePackQuantity(string orderID)
        {
            string sql = string.Format("UPDATE SC_PALLETMASTER SET PACKQUANTITY = QUANTITY,PACKQUANTITY1= QUANTITY1 WHERE SORTNO <= (SELECT MAX(SORTNO) FROM SC_PALLETMASTER WHERE ORDERID = '{0}')", orderID);
            ExecuteNonQuery(sql);
        }

        public void InsertMaster(DataTable masterTable)
        {
            ExecuteQuery("TRUNCATE TABLE SC_PALLETMASTER");
            BatchInsert(masterTable, "SC_PALLETMASTER");
        }

        public void InsertDetail(DataTable detailTable)
        {
            ExecuteQuery("TRUNCATE TABLE AS_SC_PALLETDETAIL");
            BatchInsert(detailTable, "AS_SC_PALLETDETAIL");
        }

        public void InsertOrder(DataTable orderTable)
        {
            ExecuteQuery("TRUNCATE TABLE SC_ORDER");
            BatchInsert(orderTable, "SC_ORDER");
        }

        public void InsertHandleSupply(DataTable handleSupplyOrderTable)
        {
            ExecuteQuery("TRUNCATE TABLE SC_HANDLESUPPLY");
            BatchInsert(handleSupplyOrderTable, "SC_HANDLESUPPLY");
        }

        /// <summary>
        /// 郑小龙 20110904 添加
        /// </summary>
        /// <param name="sortTable"></param>
        public void InsertSortingUpload(DataTable sortTable, string lineCode)
        {
            string sortid = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            sortid = lineCode.Substring(1, 1) + sortid.Substring(3, 5) + sortid.Substring(13, 4);
            string sql = "INSERT INTO DWV_IORD_SORT_STATUS(SORT_BILL_ID,ORG_CODE,ORDERDATE,IsImport)VALUES('{0}','{1}','{2}','0')";
            sql = string.Format(sql, sortid, sortTable.Rows[0]["ORGCODE"].ToString(), Convert.ToDateTime(sortTable.Rows[0]["ORDERDATE"].ToString()).ToString("yyyyMMdd"));
            this.ExecuteNonQuery(sql);
        }

        public int FindUnsortCount()
        {
            string sql = "SELECT COUNT(*) FROM SC_PALLETMASTER WHERE STATUS='0'";
            sql = "SELECT COUNT(*) FROM SC_ORDER_MASTER WHERE STATUS='0'";
            return Convert.ToInt32(ExecuteScalar(sql));
        }
        public int FindSortedCount()
        {
            string sql = "SELECT COUNT(*) FROM SC_ORDER_MASTER WHERE STATUS='1' OR STATUS1='1'";
            return Convert.ToInt32(ExecuteScalar(sql));
        }
        /// <summary>
        /// 矫正订单，更改订单的状态
        /// </summary>
        /// <param name="sortNo">PLC发来要矫正的流水号</param>
        /// <param name="channelGroup">A线或B线</param>
        public void UpdateMissOrderStatus(string sortNo)
        {
            string sql = string.Format("UPDATE SC_ORDER_MASTER SET STATUS = '0',STATUS1 = '0',STARTTIME=NULL,STARTTIME1=NULL,FINISHEDTIME=NULL,FINISHEDTIME1=NULL WHERE SORTNO > {0}", sortNo);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE SC_ORDER_MASTER SET STATUS = '1',STATUS1 = '1',FINISHEDTIME=GETDATE(),FINISHEDTIME1=GETDATE() WHERE SORTNO <=  {0}", sortNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 矫正订单，更改订单的状态
        /// </summary>
        /// <param name="sortNo">PLC发来要矫正的流水号</param>
        /// <param name="channelGroup">A线或B线</param>
        public void UpdateMissOrderStatus(string sortNo, string channelType)
        {
            string sql = string.Format("UPDATE SC_ORDER_MASTER SET STATUS{0} = '0',STARTTIME{0}=NULL,FINISHEDTIME{0}=NULL WHERE SORTNO > {1}", channelType == "3" ? "" : "1", sortNo);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE SC_ORDER_MASTER SET FINISHEDTIME{0}=GETDATE(),STATUS{0} = '1' WHERE SORTNO <=  {1}", channelType == "3" ? "" : "1", sortNo);
            ExecuteNonQuery(sql);
        }

        public void UpdateQuantity(string sortNo, string orderId,string channelName,string ProductCode, int quantity)
        {
            string sql = string.Format("UPDATE SC_ORDER SET QUANTITY = {0} WHERE SORTNO = {1} AND ORDERID = '{2}' AND ProductCode = '{3}' ", quantity,sortNo, orderId, ProductCode);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE SC_HANDLESUPPLY SET QUANTITY = {0} WHERE SORTNO = {1} AND ORDERID = '{2}' AND ProductCode = '{3}' ", quantity, sortNo, orderId, ProductCode);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE SC_CHANNELUSED SET QUANTITY = (SELECT SUM(QUANTITY) FROM SC_ORDER WHERE SC_ORDER.CHANNELCODE = SC_CHANNELUSED.CHANNELCODE) WHERE CHANNELNAME = '{0}' ", channelName);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE SC_PALLETMASTER SET QUANTITY = (SELECT SUM(QUANTITY) FROM SC_ORDER WHERE SORTNO = {0}) WHERE SORTNO = {0} ", sortNo);
            ExecuteNonQuery(sql);

            sql = string.Format("SELECT * FROM SC_CHANNELUSED WHERE CHANNELNAME = '{0}' " , channelName);
            DataTable channelTable = ExecuteQuery(sql).Tables[0];
            sql = string.Format("SELECT A.* FROM SC_ORDER A LEFT JOIN SC_CHANNELUSED B ON A.CHANNELCODE = B.CHANNELCODE WHERE B.CHANNELNAME = '{0}' ", channelName);
            DataTable orderTable = ExecuteQuery(sql).Tables[0];
            Util.SetChannelSortNoUtil.SetChannelSortNo(channelTable,orderTable);
            sql = string.Format("UPDATE SC_CHANNELUSED SET SORTNO = {0} WHERE CHANNELNAME = '{0}' ",channelTable.Rows[0]["SORTNO"] ,channelName);
            ExecuteNonQuery(sql);
        }
        public void UpdateQuantity(string orderDate, string batchNo)
        {
            //string sql = string.Format("UPDATE SC_ORDER SET QUANTITY = {0} WHERE SORTNO = {1} AND ORDERID = '{2}' AND ProductCode = '{3}' ", quantity, sortNo, orderId, ProductCode);
            //ExecuteNonQuery(sql);
            //sql = string.Format("UPDATE SC_HANDLESUPPLY SET QUANTITY = {0} WHERE SORTNO = {1} AND ORDERID = '{2}' AND ProductCode = '{3}' ", quantity, sortNo, orderId, ProductCode);
            //ExecuteNonQuery(sql);
            //sql = string.Format("UPDATE SC_CHANNELUSED SET QUANTITY = (SELECT SUM(QUANTITY) FROM SC_ORDER WHERE SC_ORDER.CHANNELCODE = SC_CHANNELUSED.CHANNELCODE) WHERE CHANNELNAME = '{0}' ", channelName);
            //ExecuteNonQuery(sql);
            string sql = string.Format("UPDATE SC_ORDER_MASTER SET QUANTITY = ISNULL(V_ORDER_DETAIL.QUANTITY,0),QUANTITY1=ISNULL(V_ORDER_DETAIL.QUANTITY1,0) " +
                                "FROM SC_ORDER_MASTER " +
                                "LEFT JOIN V_ORDER_DETAIL ON SC_ORDER_MASTER.ORDERDATE=V_ORDER_DETAIL.ORDERDATE AND SC_ORDER_MASTER.BATCHNO=V_ORDER_DETAIL.BATCHNO AND SC_ORDER_MASTER.ORDERID=V_ORDER_DETAIL.ORDERID " +
                                "WHERE SC_ORDER_MASTER.ORDERDATE = '{0}' AND SC_ORDER_MASTER.BATCHNO='{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);

            //sql = string.Format("UPDATE SC_ORDER_MASTER SET ABNORMITY_QUANTITY = V_ORDER_ABNORMITY.QUANTITY " +
            //                    "FROM SC_ORDER_MASTER " +
            //                    "LEFT JOIN V_ORDER_ABNORMITY ON SC_ORDER_MASTER.ORDERDATE=V_ORDER_ABNORMITY.ORDERDATE AND SC_ORDER_MASTER.BATCHNO=V_ORDER_ABNORMITY.BATCHNO AND SC_ORDER_MASTER.ORDERID=V_ORDER_ABNORMITY.ORDERID " +
            //                    "WHERE SC_ORDER_MASTER.ORDERDATE = '{0}' AND SC_ORDER_MASTER.BATCHNO='{1}'", orderDate, batchNo);
            //ExecuteNonQuery(sql);

            //更新异形烟数量

        }
        /// <summary>
        /// 查询分拣效率
        /// </summary>
        /// <returns>返回一个整数效率</returns>
        public int FindDispatchingAverage()
        {
            string sql = "SELECT ISNULL((SELECT TOP 1 分拣运行效率 AS AVERAGE FROM 效率报表 ORDER BY ID DESC),0)";
            return Convert.ToInt32(ExecuteQuery(sql).Tables[0].Rows[0][0]);
        }

        public DataTable FindOrderDetail(string orderID)
        {
            string sql = string.Format("SELECT A.ProductName, SUM(A.QUANTITY) QUANTITY FROM SC_ORDER A  LEFT JOIN dbo.SC_CHANNELUSED B ON A.CHANNELCODE = B.CHANNELCODE WHERE ORDERID = '{0}' GROUP BY A.SORTNO ,B.CHANNELNAME,A.ProductCode,A.ProductName ORDER BY A.SORTNO,B.CHANNELNAME", orderID);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrderMaster()
        {
            string sql = "SELECT A.ORDERDATE,GETDATE() CDATE,A.BATCHNO,A.LINECODE,"+
                            " A.ORDERID,AREACODE,AREANAME,ROUTECODE,ROUTENAME,CUSTOMERCODE,CUSTOMERNAME,LICENSENO,ADDRESS,"+
                            " CUSTOMERSORTNO,A.ORDERNO,ABNORMITY_QUANTITY,"+
                            " SUM(B.QUANTITY) QUANTITY0 ,"+
                            " ISNULL((SELECT SUM(QUANTITY) FROM SC_ORDER_DETAIL WHERE ORDERID = A.ORDERID AND EXPORTNO = 1),0) AS QUANTITY1," +
                            " ISNULL((SELECT SUM(QUANTITY) FROM SC_ORDER_DETAIL WHERE ORDERID = A.ORDERID AND EXPORTNO = 2),0) AS QUANTITY2" +
                            " FROM SC_ORDER_MASTER A" +
                            " LEFT JOIN SC_ORDER_DETAIL B ON A.SORTNO = B.SORTNO" +
                            " GROUP BY A.ORDERDATE,A.BATCHNO,A.LINECODE,"+
                            " A.ORDERID,AREACODE,AREANAME,ROUTECODE,ROUTENAME,CUSTOMERCODE,CUSTOMERNAME,LICENSENO,ADDRESS,"+
                            " CUSTOMERSORTNO,A.ORDERNO,ABNORMITY_QUANTITY"+
                            " ORDER BY CUSTOMERSORTNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdatePackCount(string orderId,int exportNo,int packCount)
        {
            string sql = "UPDATE SC_SORT_PACKORDER SET PACKCOUNT = {0} WHERE EXPORTNO = {1} AND ORDERID = '{2}'";
            ExecuteNonQuery(string.Format(sql,packCount,exportNo,orderId));
        }

        public void InsertPackOrder(DataRow orderMasterRow, DataRow orderDetailRow, int splitQuantity, int orderNo, int packNo, int exportNo, int packCount)
        {
            SqlCreate sql = new SqlCreate("SC_SORT_PACKORDER", SqlType.INSERT);

            sql.AppendQuote("ORDERDATE", orderMasterRow["ORDERDATE"]);
            sql.AppendQuote("CDATE", orderMasterRow["CDATE"]);
            sql.AppendQuote("BATCHNO", orderMasterRow["BATCHNO"]);
            sql.AppendQuote("LINECODE", orderMasterRow["LINECODE"]);                      
            
            sql.AppendQuote("ORDERID", orderMasterRow["ORDERID"]);
            sql.AppendQuote("AREACODE", orderMasterRow["AREACODE"]);
            sql.AppendQuote("AREANAME", orderMasterRow["AREANAME"]);
            sql.AppendQuote("ROUTECODE", orderMasterRow["ROUTECODE"]);
            sql.AppendQuote("ROUTENAME", orderMasterRow["ROUTENAME"]);
            sql.AppendQuote("CUSTOMERCODE", orderMasterRow["CUSTOMERCODE"]);
            sql.AppendQuote("CUSTOMERNAME", orderMasterRow["CUSTOMERNAME"]);

            sql.AppendQuote("LICENSENO", orderMasterRow["LICENSENO"]);
            sql.AppendQuote("ADDRESS", orderMasterRow["ADDRESS"]);
            sql.AppendQuote("CUSTOMERSORTNO", orderMasterRow["CUSTOMERSORTNO"]);
            sql.AppendQuote("CUSTOMERORDERNO", orderMasterRow["ORDERNO"]);

            sql.Append("SUMQUANTITY", orderMasterRow["QUANTITY0"]);
            sql.Append("ABNORMITY_QUANTITY", orderMasterRow["ABNORMITY_QUANTITY"]);

            sql.Append("SORTNO", orderDetailRow["SORTNO"]);
            sql.Append("CHANNELGROUP", orderDetailRow["CHANNELGROUP"]);
            sql.AppendQuote("ProductCode", orderDetailRow["ProductCode"]);
            sql.AppendQuote("ProductName", orderDetailRow["ProductName"]);

            sql.Append("QUANTITY", splitQuantity);
            sql.Append("ORDERNO", orderNo);
            sql.Append("PACKNO", packNo);
            sql.Append("PACKCOUNT",0);
            sql.Append("PACKINDEX", packCount);

            sql.Append("EXPORTNO", exportNo);            

            ExecuteNonQuery(sql.GetSQL());
        }

        public void UpdatePackOrderStatus(int exportNo)
        {
            ExecuteNonQuery(string.Format("UPDATE SC_SORT_PACKORDERSTATUS SET STATUS ='1' WHERE PACKCODE = '{0}' ", exportNo));
        }

        internal DataTable FindStartNoForCacheOrderQuery(int channelGroup, int startNo)
        {
            string sql = @"SELECT SORTNO FROM SC_ORDER WHERE CHANNELGROUP ={0} 
                        AND ORDERID IN 
                        (SELECT ORDERID FROM SC_ORDER WHERE SORTNO ={1}) 
                        AND ORDERNO IN
                        (SELECT ORDERNO FROM SC_ORDER WHERE SORTNO ={1})
                        ORDER BY SORTNO";
            return ExecuteQuery(string.Format(sql, channelGroup, startNo)).Tables[0];
        }

        internal DataTable FindOrderIDAndOrderNoForCacheOrderQuery(int channelGroup, int sortNo)
        {
            string sql = "SELECT ORDERID,ORDERNO FROM SC_ORDER WHERE  CHANNELGROUP = {0} AND SORTNO ={1}";
            return ExecuteQuery(string.Format(sql,channelGroup,sortNo)).Tables[0];
        }

        public DataTable FindDetailForCacheOrderQuery(string orderId, int orderNo, int channelGroup)
        {
            string sql = @"SELECT A.SORTNO,A.ORDERID,C.CUSTOMERNAME,B.CHANNELNAME,
                             CASE B.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE,
                             A.ProductCode, A.ProductName, A.QUANTITY ,
                                CASE WHEN A.CHANNELGROUP=1 THEN 'A线' ELSE 'B线' END  CHANNELLINE, 
                                  ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 0 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO0,
                                  ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 1 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO1,
                                  ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 2 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO2
                                    FROM SC_ORDER A
                                      LEFT JOIN SC_CHANNELUSED B 
                                        ON A.CHANNELCODE=B.CHANNELCODE
                                          LEFT JOIN SC_PALLETMASTER C
                                            ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE
                                              WHERE A.ORDERID ='{0}' AND A.ORDERNO ={1} AND A.CHANNELGROUP ={2} ORDER BY A.SORTNO DESC,B.CHANNELADDRESS DESC";
            return ExecuteQuery(string.Format(sql, orderId, orderNo,channelGroup)).Tables[0];
        }

        /// <summary>
        /// 查询多沟带缓存段
        /// </summary>
        /// <param name="channelGroup">通道组</param>
        /// <param name="sortNoStart">前端订单号</param>
        /// <returns>返回多沟带缓存段起所有订单数据</returns>         
        public DataTable FindDetailForCacheOrderQuery(int channelGroup, int sortNoStart)
        {
            string sql = @"SELECT A.SORTNO,A.ORDERID,A.ProductCode, A.ProductName, A.QUANTITY ,C.CUSTOMERNAME,B.CHANNELNAME,   
                            CASE B.CHANNELTYPE 
                        	  WHEN '2' 
	                            THEN '立式机' 
                              WHEN '5' 
	                            THEN '立式机' 
	                          ELSE '通道机' 
                            END CHANNELTYPE,   
                            CASE 
	                          WHEN A.CHANNELGROUP=1 
	                            THEN 'A线' 
	                          ELSE 'B线' 
                            END  CHANNELLINE,  
                              ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 0 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO0,  
                              ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 1 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO1,  
                              ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 2 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO2  
                                FROM SC_ORDER A  
                                  LEFT JOIN SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE   
                                    LEFT JOIN SC_PALLETMASTER C ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE
                                      WHERE A.CHANNELGROUP ={0} AND A.SORTNO >={1} ORDER BY A.SORTNO,B.CHANNELADDRESS";
            return ExecuteQuery(string.Format(sql, channelGroup, sortNoStart)).Tables[0];
        }

        public DataTable FindDetailForCacheOrderQuery(int exportNo, int sortNo, string packMode)
        {
            string sql = "";
            switch (packMode)
            {
                case "0":
                    sql = "SELECT A.SORTNO,A.ORDERID,C.CUSTOMERNAME,B.CHANNELNAME, " +
                            " CASE B.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                            " A.ProductCode, A.ProductName, A.QUANTITY ," +
                            " CASE WHEN A.CHANNELGROUP = 1 THEN 'A线' ELSE 'B线' END  CHANNELLINE, " +
                            " ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 0 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO0," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 1 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO1," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 2 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO2, " +
                            "  CASE WHEN " +
                            " 	    A.CHANNELGROUP = 1" +
                            "  THEN" +
                            " 	    CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER " +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_PALLETMASTER " +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  ELSE" +
                            "      CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER" +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_PALLETMASTER" +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY1 != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  END" +
                            "  ORDERNO_PACKNO" +
                            " FROM SC_ORDER A " +
                            " LEFT JOIN SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                            " LEFT JOIN SC_PALLETMASTER C ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE  " +
                            " WHERE A.EXPORTNO = {0} AND A.SORTNO <= {1}  ORDER BY C.CUSTOMERSORTNO DESC,ORDERNO_PACKNO DESC,A.CHANNELGROUP,A.SORTNO DESC,B.CHANNELADDRESS DESC";
                    sql = string.Format(sql, exportNo, sortNo);
                    break;
                case "1":
                    sql = "SELECT A.SORTNO,A.ORDERID,C.CUSTOMERNAME,B.CHANNELNAME, " +
                            " CASE B.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                            " A.ProductCode, A.ProductName, A.QUANTITY ," +
                            " CASE WHEN A.CHANNELGROUP = 1 THEN 'A线' ELSE 'B线' END  CHANNELLINE, " +
                            " ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 0 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO0," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 1 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO1," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM SC_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 2 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO2," +
                            "  CASE WHEN " +
                            " 	    A.CHANNELGROUP = 1" +
                            "  THEN" +
                            " 	    CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER " +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_PALLETMASTER " +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  ELSE" +
                            "      CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER" +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_PALLETMASTER" +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY1 != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  END" +
                            "  ORDERNO_PACKNO" +
                            " FROM SC_ORDER A " +
                            " LEFT JOIN SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                            " LEFT JOIN SC_PALLETMASTER C ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE  " +
                            " WHERE A.SORTNO <= {0}  ORDER BY C.CUSTOMERSORTNO DESC,ORDERNO_PACKNO DESC,A.CHANNELGROUP,A.SORTNO DESC,B.CHANNELADDRESS DESC";
                    sql = string.Format(sql,sortNo);
                    break;
                default:
                    return (new DataTable());
            }
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindPackDataOrder(int exportNo)
        {
            switch (exportNo)
            {
                case 0:
                    return ExecuteQuery("SELECT * FROM V_SORT_PACKORDER_ALL ORDER BY ORDERNO").Tables[0];
                case 1:
                    return ExecuteQuery("SELECT * FROM V_SORT_PACKORDER_PACKER_ONE ORDER BY ORDERNO").Tables[0];
                case 2:
                    return ExecuteQuery("SELECT * FROM V_SORT_PACKORDER_PACKER_TWO ORDER BY ORDERNO").Tables[0];
                default:
                    return new DataTable();
            }
        }

        /// <summary>
        /// 删除贴标机数据
        /// </summary>
        public void DeleteExportData()
        {
            ExecuteQuery("TRUNCATE TABLE SC_SORT_PACKORDER");
            ExecuteQuery("TRUNCATE TABLE AS_SC_EXPORTPACK1");
            ExecuteQuery("TRUNCATE TABLE AS_SC_EXPORTPACK2");
            ExecuteQuery("TRUNCATE TABLE AS_SC_PACKTEAR1");
            ExecuteQuery("TRUNCATE TABLE AS_SC_PACKTEAR2");
        }
        public void DeletePackData()
        {
            ExecuteQuery("UPDATE SC_SORT_PACKORDERSTATUS SET STATUS = '0'");
        }

        //internal DataTable FindOrderDetailForPack(string orderId, string exportNo)
        //{
        //    string sql = "SELECT A.* ,B.CHANNELADDRESS," +
        //         "  CASE WHEN " +
        //         " 	    A.CHANNELGROUP = 1" +
        //         "  THEN" +
        //         " 	    CASE WHEN " +
        //         " 	    (" +
        //         " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER " +
        //         " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
        //         " 	    ) = (" +
        //         " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_PALLETMASTER " +
        //         " 	    WHERE ORDERID= A.ORDERID AND QUANTITY != 0" +
        //         "      )" +
        //         " 	    THEN 10000 " +
        //         " 	    ELSE ORDERNO " +
        //         "      END " +
        //         "  ELSE" +
        //         "      CASE WHEN " +
        //         " 	    (" +
        //         " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_ORDER" +
        //         " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
        //         " 	    ) = (" +
        //         " 	    SELECT ISNULL(MAX(SORTNO),0) FROM SC_PALLETMASTER" +
        //         " 	    WHERE ORDERID= A.ORDERID AND QUANTITY1 != 0" +
        //         "      )" +
        //         " 	    THEN 10000 " +
        //         " 	    ELSE ORDERNO " +
        //         "      END " +
        //         "  END" +
        //         "  ORDERNO_PACKNO" +
        //    " FROM SC_ORDER A " +
        //    " LEFT JOIN SC_CHANNELUSED B ON A.LINECODE = B.LINECODE AND A.CHANNELCODE = B.CHANNELCODE " +
        //    " WHERE A.ORDERID = '{0}' AND A.EXPORTNO IN ({1})" +
        //    " ORDER BY ORDERNO_PACKNO,CHANNELGROUP DESC,SORTNO,CHANNELADDRESS";
        //    return ExecuteQuery(string.Format(sql,orderId,exportNo)).Tables[0];
        //}

        internal DataTable FindOrderDetailForPack(string orderId, string exportNo)
        {
            string sql = "SELECT A.* ,B.CHANNELADDRESS," +
                         " ORDERNO " +
                         " ORDERNO_PACKNO" +
                         " FROM SC_ORDER_DETAIL A " +
                         " LEFT JOIN SC_CHANNELUSED B ON A.LINECODE = B.LINECODE AND A.CHANNELCODE = B.CHANNELCODE " +
                         " WHERE A.ORDERID = '{0}' AND A.EXPORTNO IN ({1})" +
                         " ORDER BY ORDERNO_PACKNO,CHANNELGROUP DESC,SORTNO,CHANNELADDRESS";
            return ExecuteQuery(string.Format(sql, orderId, exportNo)).Tables[0];
        }

        public DataTable packOrderToExport(int packNo)
        {
            string sql = @"SELECT CONVERT(NVARCHAR(10), A.ORDERDATE, 120)AS ORDERDATE,A.BATCHNO,A.LINECODE,A.SORTNO,
                                A.ORDERID,B.PACKNO,A.ROUTECODE,A.ROUTENAME,A.CUSTOMERCODE,A.CUSTOMERNAME,
                                A.ADDRESS AS CUSTOMERADDRESS ,A.ORDERNO,
                                B.ProductCode,B.ProductName,SUM(B.QUANTITY)AS QUANTITY,
                                B.CHANNELGROUP,B.CHANNELCODE
                            FROM SC_PALLETMASTER  A 
                            LEFT JOIN SC_ORDER B ON A.SORTNO=B.SORTNO 
                            WHERE B.PACKNO={0}
								AND B.PACKNO NOT IN (SELECT PACKNO FROM AS_SC_EXPORTPACK1)
								AND B.PACKNO NOT IN (SELECT PACKNO FROM AS_SC_EXPORTPACK2)
                            GROUP BY A.ORDERDATE,A.BATCHNO,A.LINECODE,A.SORTNO,
                                A.ORDERID,B.PACKNO,A.ROUTECODE,A.ROUTENAME,A.CUSTOMERCODE,A.CUSTOMERNAME,
                                A.ADDRESS,A.ORDERNO,
                                B.ProductCode,B.ProductName,
                                B.CHANNELGROUP,B.CHANNELCODE 
                            ORDER BY B.CHANNELGROUP DESC,B.CHANNELCODE ASC";
            return ExecuteQuery(string.Format(sql, packNo)).Tables[0];
        }

        internal int GetPackOrderMaxId(int exportPackNo)
        {
            return Convert.ToInt32(ExecuteScalar(string.Format("SELECT ISNULL(MAX(IDENTITYNO),0) + 1 FROM AS_SC_EXPORTPACK{0}", exportPackNo)));
        }

        private static string strsql = "";
        public void   InsertPackExport(DataRow InRow,int exportNo,int customerSumQuantity,int bagSumQuantity,int packOrderMaxId)
        {
            SqlCreate sql = new SqlCreate(string.Format("AS_SC_EXPORTPACK{0}", exportNo), SqlType.INSERT);
            sql.Append("IDENTITYNO", packOrderMaxId);
            sql.AppendQuote("ORDERDATE", InRow["ORDERDATE"]);
            sql.Append("BATCHNO", InRow["BATCHNO"]);
            sql.AppendQuote("LINECODE", InRow["LINECODE"]);
            sql.AppendQuote("SORTNO", InRow["SORTNO"]);
            sql.AppendQuote("ORDERID", InRow["ORDERID"]);
            sql.AppendQuote("ROUTECODE", InRow["ROUTECODE"]);
            sql.AppendQuote("ROUTENAME", InRow["ROUTENAME"]);
            sql.AppendQuote("CUSTOMERCODE", InRow["CUSTOMERCODE"]);
            sql.AppendQuote("CUSTOMERNAME", InRow["CUSTOMERNAME"]);
            sql.AppendQuote("CUSTOMERSORTNO", InRow["ORDERNO"]);
            sql.AppendQuote("CUSTOMERADDRESS", InRow["CUSTOMERADDRESS"]);
            sql.AppendQuote("ProductCode", InRow["ProductCode"]);
            sql.AppendQuote("ProductName", InRow["ProductName"]);
            sql.AppendQuote("BAGQUANTITY", bagSumQuantity);
            sql.AppendQuote("TQUANTITY", customerSumQuantity);
            sql.AppendQuote("PACKNO", InRow["PACKNO"]);
            sql.AppendQuote("QUANTITY", InRow["QUANTITY"]);

            if (strsql == sql.GetSQL())
            {
                MCP.Logger.Info(System.Threading.Thread.CurrentThread.Name);
                MCP.Logger.Info(sql.GetSQL());
            }
            strsql = sql.GetSQL();

            ExecuteNonQuery(sql.GetSQL());
        }

        /// <summary>
        /// 客户卷烟量
        /// </summary>
        /// <param name="customerCode">客户包</param>
        /// <returns>客户分拣卷烟总量</returns>
        public int FindCustomerQuantity(int packNo)
        {
            string sql = "SELECT SUM(QUANTITY)AS TQUANTITY FROM SC_ORDER WHERE ORDERID IN(SELECT ORDERID FROM SC_ORDER WHERE PACKNO={0})";
            return Convert.ToInt32(ExecuteQuery(string.Format(sql, packNo)).Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 烟包卷烟烟量
        /// </summary>
        /// <param name="customerCode">客户包</param>
        /// <returns>烟包卷烟量</returns>
        public int FindBagQuantity(int packNo)
        {
            string sql = "SELECT SUM(QUANTITY)AS TQUANTITY FROM SC_ORDER WHERE PACKNO={0}";
            return Convert.ToInt32(ExecuteQuery(string.Format(sql, packNo)).Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 查找已传给贴标的包号
        /// </summary>
        /// <param name="exportNo">包装机号</param>
        /// <returns>包号表</returns>
        public DataTable FindexportPack(int exportNo)
        {
            string sql = " SELECT DISTINCT PACKNO FROM dbo.AS_SC_EXPORTPACK{0}";
            return ExecuteQuery(string.Format(sql,exportNo)).Tables[0];
        }

        /// <summary>
        /// 查找烟包的数据行
        /// </summary>
        /// <param name="exportNo">出口终端号</param>
        /// <param name="packNo">包号</param>
        /// <returns></returns>
        public int FindCountDataByPackNo(int exportNo,int packNo)
        {
            string sql = "SELECT COUNT(*) FROM dbo.AS_SC_EXPORTPACK{0} WHERE PACKNO={1}";
            return Convert.ToInt32(ExecuteQuery(string.Format(sql,exportNo, packNo)).Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataTable GetOrderMain(string orderDate, string batchNo, string lineCode)
        {
            string sql = "INSERT INTO SC_ORDER_MASTER(ORDERDATE, BATCHNO, LINECODE, SORTNO, ORDERID, AREACODE, AREANAME, ROUTECODE, ROUTENAME, CUSTOMERCODE, " +
                         "CUSTOMERNAME, ADDRESS, LICENSENO, CUSTOMERSORTNO, ORDERNO, QUANTITY,ABNORMITY_QUANTITY) " +
                         "SELECT ORDERDATE, BATCHNO, LINECODE, SORTNO, ORDERID, AREACODE, AREANAME, ROUTECODE, ROUTENAME, CUSTOMERCODE, " +
                         "CUSTOMERNAME, ADDRESS, LICENSENO, SORTNO, SORTNO, QUANTITY,ABNORMITY_QUANTITY " +
                         "FROM V_ORDER_MASTER WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}' AND LINECODE='{2}'";
            sql = string.Format(sql, orderDate, batchNo, lineCode);

            int rows =  ExecuteNonQuery(sql);

            sql = string.Format("SELECT * from V_ORDER_MASTER Where ORDERDATE = '{0}' AND BATCHNO = '{1}' AND LINECODE='{2}' ORDER BY SORTNO", orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataTable GetOrderQuantity5(string batchNo)
        {
            string sql = @"select ProductCode,ProductName,
                           case when channelCount=2 or channelCount=3 then sum(eachquantity5*5+(case when channelCount=2 and eachquantity51=1 then 5 else 0 end)) else 0 end FChannelQty,
                           case when channelCount=3 then sum(eachquantity5*5+(case when channelCount=3 and eachquantity51=2 then 5 else 0 end)) else 0 end SChannelQty
                           from V_ORDER_DETAIL5 
                           where batchno='{0}'
                           group by ProductCode,ProductName,channelCount";
            sql = string.Format(sql, batchNo);

            return ExecuteQuery(sql).Tables[0];            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet GetSortingOrder()
        {
            string sql = "SELECT TOP 4 CUSTOMERNAME,ORDERID,SORTNO,AREANAME,ROUTENAME,QUANTITY FROM SC_ORDER_MASTER " +
                         "WHERE (STATUS=0 OR STATUS1=0) ORDER BY SORTNO;SELECT * FROM V_SC_ORDER_DETAIL WHERE SORTNO IN " +
                         "(SELECT TOP 4 SORTNO FROM SC_ORDER_MASTER WHERE (STATUS=0 OR STATUS1=0) ORDER BY SORTNO) ORDER BY CHANNELCODE";

            sql = "SELECT TOP 4 BATCHNO,CUSTOMERNAME,ORDERID,SORTNO,AREANAME,ROUTENAME,QUANTITY FROM SC_ORDER_MASTER " +
                         "WHERE ((FINISHEDTIME IS NULL AND QUANTITY>0) OR (FINISHEDTIME1 IS NULL AND QUANTITY1>0)) ORDER BY BATCHNO,SORTNO";
            //sql = string.Format(sql, SortNo);

            DataSet ds = ExecuteQuery(sql);

            return ds;
        }
        public DataTable GetSortingOrderDetail(string SortNo)
        {
            string sql = "SELECT * FROM V_SC_ORDER_DETAIL WHERE SORTNO={0} ORDER BY CHANNELCODE";


            sql = string.Format(sql, SortNo);

            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable GetSortingOrderDetail(string batchNo,string SortNo)
        {
            string sql = "SELECT * FROM V_SC_ORDER_DETAIL WHERE BATCHNO='{0}' AND SORTNO={1} ORDER BY CHANNELCODE";


            sql = string.Format(sql, batchNo,SortNo);

            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet GetOrder(string SortNo)
        {
            string sql = "SELECT CUSTOMERNAME,ORDERID,SORTNO,AREANAME,ROUTENAME,QUANTITY,QUANTITY1 FROM SC_ORDER_MASTER WHERE SORTNO={0};SELECT * FROM V_SC_ORDER_DETAIL WHERE SORTNO={0} ORDER BY CHANNELCODE";
            sql = string.Format(sql, SortNo);

            DataSet ds = ExecuteQuery(sql);
            
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet GetOrder(string batchNo,string SortNo)
        {
            string sql = "SELECT BATCHNO,CUSTOMERNAME,ORDERID,SORTNO,AREANAME,ROUTENAME,QUANTITY,QUANTITY1 FROM SC_ORDER_MASTER WHERE BATCHNO='{0}' AND SORTNO={1};SELECT * FROM V_SC_ORDER_DETAIL WHERE BATCHNO='{0}' AND SORTNO={1} ORDER BY BATCHNO,CHANNELCODE";
            sql = string.Format(sql, batchNo,SortNo);

            DataSet ds = ExecuteQuery(sql);

            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet GetOrder(string orderDate, string batchNo, string SortNo)
        {
            string sql = "SELECT * FROM SC_ORDER_MASTER WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}' AND SORTNO={2}";
            sql = string.Format(sql, orderDate, batchNo, SortNo);

            DataSet ds = ExecuteQuery(sql);

            sql = "SELECT * FROM SC_ORDER_DETAIL WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}' AND SORTNO={2} ORDER BY CHANNELCODE";
            sql = string.Format(sql, orderDate, batchNo, SortNo);
            ds = ExecuteQuery(sql);
            return ds;
        }
        /// <summary>
        /// 获取订单明细
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <param name="ChannelType"></param>
        /// <returns></returns>
        public DataTable GetChannelOrderDetail(string orderDate, string batchNo, string lineCode)
        {
            string sql = "SELECT A.*,B.CHANNELADDRESS,B.QUANTITY,B.CHANNELORDER,B.CHANNELTYPE,C.BARCODE,C.BARCODEPACK " +
                         "FROM SC_ORDER_DETAIL A " +
                         "INNER JOIN SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE AND A.BATCHNO=B.BATCHNO " +
                         "LEFT JOIN CMD_Product C ON A.ProductCode=C.ProductCode " +
                         "Where A.ORDERDATE = '{0}' AND A.BATCHNO = '{1}' AND A.LINECODE='{2}' " +
                         "ORDER BY A.SORTNO,B.CHANNELADDRESS";
            sql = string.Format(sql, orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取订单明细
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <param name="ChannelType"></param>
        /// <returns></returns>
        public DataTable GetChannelOrderDetail2(string orderDate, string batchNo, string lineCode)
        {
            string sql = "SELECT  A.ORDERDATE, A.BATCHNO, A.LINECODE, A.SORTNO, A.ORDERID, A.ORDERNO, A.ProductCode, A.ProductName, " +
                         "(SELECT TOP 1 CHANNELCODE FROM SC_CHANNELUSED WHERE CHANNELTYPE='2' AND BATCHNO=A.BATCHNO AND ProductCode=A.ProductCode ORDER BY CHANNELCODE) CHANNELCODE," +
                         "(SELECT TOP 1 CHANNELORDER FROM SC_CHANNELUSED WHERE CHANNELTYPE='2' AND BATCHNO=A.BATCHNO AND ProductCode=A.ProductCode ORDER BY CHANNELCODE) CHANNELORDER," +
                         "A.CHANNELGROUP, SUM(A.QUANTITY) QUANTITY,C.BARCODE,C.BARCODEPACK " +
                         "FROM SC_ORDER_DETAIL A " +
                         "INNER JOIN SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE AND A.BATCHNO=B.BATCHNO " +
                         "LEFT JOIN CMD_Product C ON A.ProductCode=C.ProductCode " +
                         "WHERE A.ORDERDATE = '{0}' AND A.BATCHNO = '{1}' AND A.LINECODE='{2}' AND B.CHANNELTYPE='2' " +
                         "GROUP BY A.ORDERDATE, A.BATCHNO, A.LINECODE, A.SORTNO, A.ORDERID, A.ORDERNO, A.ProductCode, A.ProductName,A.CHANNELGROUP,C.BARCODE,C.BARCODEPACK " +
                         "ORDER BY A.SORTNO,MIN(A.CHANNELCODE)";

            sql = string.Format(sql, orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取订单明细
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <param name="ChannelType"></param>
        /// <returns></returns>
        public DataTable InsertSupply2(string orderDate, string batchNo, string lineCode)
        {
            string sql = "INSERT INTO SC_SUPPLY2(ORDERDATE, BATCHNO, LINECODE, SERIALNO, SORTNO, ProductCode, ProductName, CHANNELCODE, CHANNELGROUP, GROUPNO, " +
                         "BARCODE, STATUS, FINISHEDTIME, ISBALANCE)" +
                         "SELECT  A.ORDERDATE, A.BATCHNO, A.LINECODE, row_number() over(order by MIN(A.SORTNO),MIN(A.CHANNELCODE)),MIN(A.SORTNO) SORTNO, A.ProductCode, A.ProductName, " +
                         "(SELECT TOP 1 CHANNELCODE FROM SC_CHANNELUSED WHERE CHANNELTYPE='2' AND BATCHNO=A.BATCHNO AND ProductCode=A.ProductCode ORDER BY CHANNELCODE) CHANNELCODE," +
                         "(SELECT TOP 1 CHANNELORDER FROM SC_CHANNELUSED WHERE CHANNELTYPE='2' AND BATCHNO=A.BATCHNO AND ProductCode=A.ProductCode ORDER BY CHANNELCODE) CHANNELORDER," +
                         "A.CHANNELGROUP, SUM(A.QUANTITY) QUANTITY,C.BARCODE,C.BARCODEPACK " +
                         "FROM SC_ORDER_DETAIL A " +
                         "INNER JOIN SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE AND A.BATCHNO=B.BATCHNO " +
                         "LEFT JOIN CMD_Product C ON A.ProductCode=C.ProductCode " +
                         "WHERE A.ORDERDATE = '{0}' AND A.BATCHNO = '{1}' AND A.LINECODE='{2}' AND B.CHANNELTYPE='2' " +
                         "GROUP BY A.ORDERDATE, A.BATCHNO, A.LINECODE, A.ProductCode, A.ProductName,A.CHANNELGROUP,C.BARCODE,C.BARCODEPACK " +
                         "ORDER BY MIN(A.SORTNO),MIN(A.CHANNELCODE)";

            sql = string.Format(sql, orderDate, batchNo, lineCode);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取订单明细
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <param name="ChannelType"></param>
        /// <returns></returns>
        public DataTable GetChannelOrderDetail(string orderDate, string batchNo, string lineCode,string ChannelType)
        {
            string sql = "SELECT A.*,B.CHANNELADDRESS,B.QUANTITY,B.CHANNELORDER " +
                         "FROM SC_ORDER_DETAIL A " +
                         "INNER JOIN SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                         "Where A.ORDERDATE = '{0}' AND A.BATCHNO = '{1}' AND A.LINECODE='{2}' AND  B.CHANNELTYPE='{3}' " +
                         "ORDER BY A.SORTNO,B.CHANNELADDRESS";
            sql = string.Format(sql, orderDate, batchNo, lineCode,ChannelType);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取下载订单统计表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindDownloadTotal()
        {
            string sql = "SELECT  ROW_NUMBER() OVER (PARTITION BY A.BATCHNO ORDER BY A.BATCHNO DESC, A.PRODUCT_PERCENT DESC) ROWID, * FROM V_ORDER_INITOTAL A ORDER BY BATCHNO DESC,PRODUCT_PERCENT DESC";
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindDownloadMaster()
        {
            string sql = "SELECT ROW_NUMBER() OVER(PARTITION BY A.BATCHNO ORDER BY A.BATCHNO,A.ORDERID) ROWID,A.ORDERDATE, A.DELIVERYDATE, A.BATCHNO, A.ORDERID, A.ROUTECODE, A.CUSTOMERCODE, C.CUSTOMERNAME, ROW_NUMBER() OVER(ORDER BY A.ORDERID) SORTNO,B.ROUTENAME, " +
                         "(SELECT SUM(QUANTITY) FROM SC_I_ORDERDETAIL WHERE ORDERID=A.ORDERID) AMOUNT " +
                         "FROM SC_I_ORDERMASTER A " +
                         "LEFT JOIN CMD_ROUTE B ON A.ROUTECODE=B.ROUTECODE " +
                         "LEFT JOIN CMD_Customer C ON A.CustomerCode=C.CustomerCode " +
                         "ORDER BY BATCHNO DESC,ROUTECODE,ORDERID";
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取下载订单明细
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataTable FindDownloadDetail(string OrderId)
        {
            string sql = string.Format("SELECT *," +
                                       "(SELECT (CASE ISABNORMITY WHEN 0 THEN '否' ELSE '是' END) FROM CMD_Product WHERE ProductCode=SC_I_ORDERDETAIL.ProductCode) ISABNORMITY " +
                                       "FROM SC_I_ORDERDETAIL WHERE ORDERID='{0}' ORDER BY ProductCode", OrderId);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindDownloadMaster(string batchNo)
        {
            string sql = string.Format("SELECT A.ORDERDATE, A.DELIVERYDATE, A.BATCHNO, A.ORDERID, A.ROUTECODE, A.CUSTOMERCODE, A.CUSTOMERDESC, A.SORTID,B.ROUTENAME, " + 
                                       "(SELECT SUM(QUANTITY) FROM SC_I_ORDERDETAIL WHERE ORDERID=A.ORDERID) AMOUNT " +
                                       "FROM SC_I_ORDERMASTER A " +
                                       "LEFT JOIN CMD_ROUTE B ON A.ROUTECODE=B.ROUTECODE " +
                                       "LEFT JOIN SC_HANDLE_SORT_ORDER C ON A.BATCHNO=C.BATCHNO AND A.ORDERID=C.ORDERID " +
                                       "WHERE A.BATCHNO='{0}' AND C.ORDERID IS NULL " +
                                       "ORDER BY SORTID", batchNo);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取下载订单明细
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataTable FindDownloadDetail(string batchNo,string OrderId)
        {
            string sql = string.Format("SELECT *," +
                                       "(SELECT SORTID FROM SC_I_ORDERMASTER WHERE ORDERID=SC_I_ORDERDETAIL.ORDERID) SORTNO," +
                                       "(SELECT (CASE ISABNORMITY WHEN 0 THEN '否' ELSE '是' END) FROM CMD_Product WHERE ProductCode=SC_I_ORDERDETAIL.ProductCode) ISABNORMITY " +
                                       "FROM SC_I_ORDERDETAIL " +
                                       "WHERE BATCHNO='{0}' AND ORDERID='{1}' ORDER BY ProductCode", batchNo, OrderId);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取手工分拣订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindHandMaster()
        {
            string sql = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY A.ORDERID) SORTNO,A.ORDERDATE, A.DELIVERYDATE, A.BATCHNO, A.ORDERID, A.ROUTECODE, A.CUSTOMERCODE, D.CUSTOMERNAME, C.ROUTENAME, " +
                                       "(SELECT SUM(QUANTITY) FROM SC_I_ORDERDETAIL WHERE ORDERID=A.ORDERID) AMOUNT " +
                                       "FROM SC_I_ORDERMASTER A " +
                                       "INNER JOIN SC_HANDLE_SORT_ORDER B ON A.BATCHNO=B.BATCHNO AND A.ORDERID=B.ORDERID " +
                                       "LEFT JOIN CMD_ROUTE C ON A.ROUTECODE=C.ROUTECODE " +
                                       "LEFT JOIN CMD_Customer D ON A.CustomerCode=D.CustomerCode " +
                                       "ORDER BY C.SORTID,A.ROUTECODE");
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取手工分拣订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindHandMaster(string batchNo)
        {
            string sql = string.Format("SELECT A.ORDERDATE, A.DELIVERYDATE, A.BATCHNO, A.ORDERID, A.ROUTECODE, A.CUSTOMERCODE, A.CUSTOMERDESC, A.SORTID,C.ROUTENAME, " +
                                       "(SELECT SUM(QUANTITY) FROM SC_I_ORDERDETAIL WHERE ORDERID=A.ORDERID) AMOUNT " +
                                       "FROM SC_I_ORDERMASTER A " +
                                       "INNER JOIN SC_HANDLE_SORT_ORDER B ON A.BATCHNO=B.BATCHNO AND A.ORDERID=B.ORDERID " +
                                       "LEFT JOIN CMD_ROUTE C ON A.ROUTECODE=C.ROUTECODE " +
                                       "WHERE A.BATCHNO='{0}' " +
                                       "ORDER BY SORTID", batchNo);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取手工分拣订单明细
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataTable FindHandDetail(string batchNo, string OrderId)
        {
            string sql = string.Format("SELECT * FROM SC_I_ORDERDETAIL A " +
                                       "INNER JOIN SC_HANDLE_SORT_ORDER B ON A.BATCHNO=B.BATCHNO AND A.ORDERID=B.ORDERID " +
                                       "WHERE A.BATCHNO='{0}' AND A.ORDERID='{1}' ORDER BY ProductCode", batchNo, OrderId);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteHandSort(string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_HANDLE_SORT_ORDER WHERE BATCHNO = '{0}'", batchNo);
            ExecuteNonQuery(sql);            
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindAbnormityOrder()
        {
            string sql = string.Format("SELECT * FROM V_ORDER_ABNORMITY_DETAIL A ORDER BY A.ORDERDATE DESC,A.ROUTECODE,A.SORTNO,A.ORDERID,A.PRODUCTCODE");
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindAbnormityOrder(string batchNo)
        {
            string sql = string.Format("SELECT * FROM V_ORDER_ABNORMITY_DETAIL A WHERE A.BATCHNO='{0}' ORDER BY A.SORTID,A.ORDERID,A.ProductCode", batchNo);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindAbnormityTotal()
        {
            string sql = string.Format("SELECT * FROM V_ORDER_ABNORMITY_TOTAL A ORDER BY A.ORDERDATE DESC,A.BATCHNO");
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindAbnormityTotal(string batchNo)
        {
            string sql = string.Format("SELECT * FROM V_ORDER_ABNORMITY A WHERE A.BATCHNO='{0}' ORDER BY A.ORDERDATE DESC,A.BATCHNO,A.PRODUCTCODE", batchNo);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取完成还没有上报的订单
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataSet FindOrderProcess(string batchNo,string sortNo)
        {
            string sql = @"SELECT CONVERT(VARCHAR(10),GETDATE(),120) AS SORT_DATE, SM1.LINECODE SORTLINE_CODE,L.LINENAME SORTLINE_DESC,BATCHNO BATCH_NO, 
                              (SELECT SUM(QUANTITY) FROM SC_I_ORDERDETAIL WHERE BATCHNO = SM1.BATCHNO) AS QTY_PRODUCT_TOT,
                              (SELECT COUNT(DISTINCT ROUTECODE) FROM SC_ORDER_MASTER WHERE BATCHNO = SM1.BATCHNO) AS QTY_ROUTE_TOT,
                              (SELECT COUNT(DISTINCT CUSTOMERCODE) FROM SC_ORDER_MASTER WHERE BATCHNO = SM1.BATCHNO) AS QTY_CUSTOMER_TOT,
                              (SELECT SUM(A.QUANTITY) FROM SC_I_ORDERDETAIL AS A 
                                                      LEFT OUTER JOIN CMD_Product AS B ON A.ProductCode = B.ProductCode
                                                      WHERE A.BATCHNO = SM1.BATCHNO AND B.ISABNORMITY = '1') +
                              (SELECT ISNULL(SUM(QUANTITY + QUANTITY1), 0) FROM SC_ORDER_MASTER WHERE FINISHEDTIME IS NOT NULL AND FINISHEDTIME1 IS NOT NULL) AS QTY_PRODUCT,
                              (SELECT COUNT(DISTINCT ROUTECODE) FROM SC_ORDER_MASTER WHERE FINISHEDTIME IS NOT NULL AND FINISHEDTIME1 IS NOT NULL) AS QTY_ROUTE,
                              (SELECT COUNT(DISTINCT CUSTOMERCODE) FROM SC_ORDER_MASTER WHERE FINISHEDTIME IS NOT NULL AND FINISHEDTIME1 IS NOT NULL) AS QTY_CUSTOMER,
                              (SELECT CUSTOMERCODE FROM SC_ORDER_MASTER WHERE BATCHNO = SM1.BATCHNO AND SORTNO={1}) AS CUSTOMER_CODE,
                              (SELECT CUSTOMERNAME FROM SC_ORDER_MASTER WHERE BATCHNO = SM1.BATCHNO AND SORTNO={1}) AS CUSTOMER_DESC,
                              (SELECT ROUTECODE FROM SC_ORDER_MASTER WHERE BATCHNO = SM1.BATCHNO AND SORTNO={1}) AS ROUTE_CODE,
                              (SELECT ROUTENAME FROM SC_ORDER_MASTER WHERE BATCHNO = SM1.BATCHNO AND SORTNO={1}) AS ROUTE_NAME, 
                              '' AS TASK_NO, 15000 AS EFFICIENCY, 1 AS STATUS, GETDATE() AS RECEIVE_TIME
                        FROM  SC_ORDER_MASTER AS SM1
                        LEFT JOIN CMD_LINEINFO L ON SM1.LINECODE=L.LINECODE
                        WHERE BATCHNO='{0}' AND (FINISHEDTIME IS NOT NULL OR QUANTITY=0) AND (FINISHEDTIME1 IS NOT NULL OR QUANTITY1=0) AND SORTNO={1} AND ISUPLOAD=0
                        GROUP BY BATCHNO, SM1.LINECODE,L.LINENAME";

            sql = string.Format(sql, batchNo, sortNo);
            return ExecuteQuery(sql);
        }
        public DataTable FindNoCigaretteChannel(string batchNo)
        {
            string sql = string.Format("SELECT * FROM V_CIGARETTE_CHANNEL A WHERE A.BATCHNO='{0}'", batchNo);
            return ExecuteQuery(sql).Tables[0];
        }
        public DataSet FindOrderStatus()
        {
            string sql = string.Format("SELECT * FROM V_ORDER_STATUS A ORDER BY SORT_ORDER");
            return ExecuteQuery(sql);
        }
        public DataSet FindOrderStatus(string batchNo)
        {
            string sql = string.Format("SELECT * FROM V_ORDER_STATUS A WHERE A.BATCH_NO='{0}' ORDER BY SORT_ORDER", batchNo);
            return ExecuteQuery(sql);
        }
        public DataTable FindChannelMaxSortNo()
        {
            //最后一单处理有问题 2015/03/21注释，修正
            string sql = @"SELECT A.CHANNELCODE,ISNULL(MAX(SORTNO),0) SORTNO
                           FROM  CMD_CHANNEL A 
                           LEFT JOIN SC_ORDER_DETAIL B ON A.CHANNELCODE=B.CHANNELCODE
                           WHERE A.CHANNELTYPE='3'
                           GROUP BY A.LINECODE, A.CHANNELCODE
                           ORDER BY A.CHANNELCODE";

            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindChannelMaxSortNo(int minQty)
        {
            //最后一单处理有问题 2015/03/21修正
            string sql = @"SELECT CMD_CHANNEL.CHANNELCODE, ISNULL(MIN(SORTNO) + 1,0) AS SORTNO 
                           FROM CMD_CHANNEL
                           LEFT JOIN V_ORDER_BALANCE ON CMD_CHANNEL.CHANNELCODE=V_ORDER_BALANCE.CHANNELCODE AND V_ORDER_BALANCE.BALANCE < {0} 
                           WHERE CMD_CHANNEL.CHANNELTYPE='3'
                           GROUP BY CMD_CHANNEL.CHANNELCODE 
                           ORDER BY CMD_CHANNEL.CHANNELCODE";

            sql = string.Format(sql, minQty);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 获取订单剩余量已经补货剩余量
        /// </summary>
        /// <param name="sortNo"></param>
        /// <param name="bNo"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable GetChannelBalance(string sortNo, int bNo,string batchNo)
        {
            string sql = @"SELECT SC_ORDER_DETAIL.BATCHNO, SC_ORDER_DETAIL.LINECODE,SC_ORDER_DETAIL.CHANNELCODE, SC_ORDER_DETAIL.ProductCode, dbo.SC_ORDER_DETAIL.ProductName,  
                           (SELECT COUNT(*) FROM SC_SUPPLY WHERE CHANNELCODE=SC_ORDER_DETAIL.CHANNELCODE AND BATCHNO=SC_ORDER_DETAIL.BATCHNO)*50+ISNULL(SC_CHANNELBALANCE.QUANTITY{0},0)-SUM(SC_ORDER_DETAIL.QUANTITY) BALANCE,
                           FROM  SC_ORDER_DETAIL  
                           LEFT OUTER JOIN SC_CHANNELBALANCE ON SC_ORDER_DETAIL.CHANNELCODE = SC_CHANNELBALANCE.CHANNELCODE AND SC_ORDER_DETAIL.BATCHNO = SC_CHANNELBALANCE.BATCHNO1
                           WHERE SC_ORDER_DETAIL.CHANNELCODE LIKE '1%' AND SC_ORDER_DETAIL.SORTNO<{1} AND SC_ORDER_DETAIL.BATCHNO='{2}'
                           GROUP BY SC_ORDER_DETAIL.BATCHNO,SC_ORDER_DETAIL.LINECODE, SC_ORDER_DETAIL.CHANNELCODE, SC_ORDER_DETAIL.ProductCode, SC_ORDER_DETAIL.ProductName, 
                           SC_CHANNELBALANCE.QUANTITY{0}
                           ORDER BY SC_ORDER_DETAIL.CHANNELCODE";

            sql = string.Format(sql, bNo - 1 > 0 ? (bNo - 1).ToString() : "", sortNo, batchNo);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 更新分拣订单结束
        /// </summary>
        /// <param name="sortNo"></param>
        /// <param name="bNo"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public void UpdateOrderState()
        {
            string sql = @"UPDATE SC_ORDER_MASTER SET STATUS='1',STATUS1='1',FINISHEDTIME=GETDATE(),FINISHEDTIME1=GETDATE()";

            ExecuteNonQuery(sql);
        }
    }
}
