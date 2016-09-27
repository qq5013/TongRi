using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class CigaretteDao : BaseDao
    {
        public DataTable FindAll()
        {
            string sql = "SELECT *,(CASE ISABNORMITY WHEN '0' THEN '·ñ' ELSE 'ÊÇ' END) ABNORMITY FROM V_CMD_Product";
            return ExecuteQuery(sql, "CMD_Product").Tables[0];
        }
        public DataTable FindAll(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT *,(CASE ISABNORMITY WHEN '0' THEN '·ñ' ELSE 'ÊÇ' END) ABNORMITY FROM V_CMD_Product" + where;
            return ExecuteQuery(sql, "CMD_Product").Tables[0];
        }
        public DataTable FindAll(int startRecord, int pageSize, string filter)
        {
             string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT *,(CASE ISABNORMITY WHEN '0' THEN '·ñ' ELSE 'ÊÇ' END) ABNORMITY FROM V_CMD_Product" + where;
            return ExecuteQuery(sql, "CMD_Product", startRecord, pageSize).Tables[0];
        }

        public int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM CMD_Product " + where;
            return (int)ExecuteScalar(sql);
        }

        public void UpdateEntity(string cigaretteCode, string cigaretteName, string showName, string isAbnormity, string barcode,int packagenum)
        {
            SqlCreate sqlCreate = new SqlCreate("CMD_Product", SqlType.UPDATE);
            sqlCreate.AppendQuote("CIGARETTENAME", cigaretteName);
            sqlCreate.AppendQuote("SHOWNAME", showName);
            sqlCreate.AppendQuote("ISABNORMITY", isAbnormity);
            sqlCreate.AppendQuote("BARCODE", barcode);
            sqlCreate.Append("PACKAGENUM", packagenum);
            sqlCreate.AppendWhereQuote("CIGARETTECODE", cigaretteCode);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }

        public void BatchInsertCigarette(DataTable dtData)
        {
            BatchInsert(dtData, "CMD_Product");
        }

        public void Clear()
        {
            string sql = "TRUNCATE TABLE CMD_Product";
            ExecuteNonQuery(sql);
        }

        //internal void SynchronizeCigarette(DataTable cigaretteTable)
        //{
        //    foreach (DataRow row in cigaretteTable.Rows)
        //    {
        //        string sql = "IF '{0}' IN (SELECT CIGARETTECODE FROM CMD_Product) " +
        //                        "BEGIN " +
        //                             "UPDATE CMD_Product SET CIGARETTENAME = '{1}',SHORTCODE='{2}', SHORTNAME='{3}', MAKERCODE='{4}', MAKERDESC='{5}', PROVINCE='{6}', ISPROVINCE='{7}',VARIETYCODE='{8}', VARIETYNAME='{9}', BARCODE='{10}', BARCODEPACK='{11}', GRADEID='{12}', STATUS='{13}', ISPICK='1', PURCHASEPRICE={14}, TRADEPRICE={15}, RETAILPRICE={16}, PACKAGENUM={17}, PALLETNUM={18} WHERE CIGARETTECODE = '{0}' " +
        //                        "END " +
        //                     "ELSE " +
        //                        "BEGIN " +
        //                            "INSERT CMD_Product (CIGARETTECODE, CIGARETTENAME, SHORTCODE, SHORTNAME, MAKERCODE, MAKERDESC, PROVINCE, ISPROVINCE,VARIETYCODE, VARIETYNAME, BARCODE, BARCODEPACK, GRADEID, STATUS, ISPICK, PURCHASEPRICE, TRADEPRICE, RETAILPRICE, PACKAGENUM, PALLETNUM,ISABNORMITY) " +
        //                            "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','1',{14},{15},{16},{17},{18},0) " +
        //                        "END";
        //        sql = string.Format(sql, row["PRODUCT_CODE"], row["PRODUCT_DESC"], row["SHORT_CODE"], row["SHORT_NAME"], row["MAKER_CODE"], row["MAKER_DESC"], row["PROVINCE"], row["IS_PROVINCE"], row["VARIETY_CODE"], row["VARIETY_NAME"], row["BARCODE_LOAF"], row["BARCODE_PACK"], row["GRADE_ID"], row["STATUS"], 0, 0, 0, row["PACKAGE_NUM"], row["PALLET_NUM"]);
        //        ExecuteNonQuery(sql);
        //    }
        //}
        internal void SynchronizeCigarette(DataTable cigaretteTable)
        {
            foreach (DataRow row in cigaretteTable.Rows)
            {
                string sql = "IF '{0}' IN (SELECT CIGARETTECODE FROM CMD_Product) " +
                                "BEGIN " +
                                     "UPDATE CMD_Product SET CIGARETTENAME = '{1}',SHORTCODE='{2}', SHORTNAME='{3}', MAKERCODE='{4}', MAKERDESC='{5}', PROVINCE='{6}', ISPROVINCE='{7}',VARIETYCODE='{8}', VARIETYNAME='{9}', BARCODE='{10}', BARCODEPACK='{11}', GRADEID='{12}', STATUS='{13}', ISPICK='1', PURCHASEPRICE={14}, TRADEPRICE={15}, RETAILPRICE={16}, PACKAGENUM={17}, PALLETNUM={18} WHERE CIGARETTECODE = '{0}' " +
                                "END " +
                             "ELSE " +
                                "BEGIN " +
                                    "INSERT CMD_Product (CIGARETTECODE, CIGARETTENAME, SHORTCODE, SHORTNAME, MAKERCODE, MAKERDESC, PROVINCE, ISPROVINCE,VARIETYCODE, VARIETYNAME, BARCODE, BARCODEPACK, GRADEID, STATUS, ISPICK, PURCHASEPRICE, TRADEPRICE, RETAILPRICE, PACKAGENUM, PALLETNUM,ISABNORMITY,SHOWNAME) " +
                                    "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','1',{14},{15},{16},{17},{18},0,'{1}') " +
                                "END";
                sql = string.Format(sql, row["PRODUCT_CODE"], row["PRODUCT_DESC"], row["SHORT_CODE"], row["SHORT_NAME"], row["MAKER_CODE"], row["MAKER_DESC"], row["PROVINCE"], row["IS_PROVINCE"], row["VARIETY_CODE"], row["VARIETY_NAME"], row["BARCODE_LOAF"], row["BARCODE_PACK"], row["GRADE_ID"], row["STATUS"], 0, 0, 0, row["PACKAGE_NUM"], row["PALLET_NUM"]);
                ExecuteNonQuery(sql);
            }
        }
    }
}
