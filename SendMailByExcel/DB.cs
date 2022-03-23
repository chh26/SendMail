using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XQLiteMgm.Models.UnitOfWork;

namespace SendMailByExcel
{
    public class DB
    {
        private SQLUnitOfWork _unitOfWork;

        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            using (var uow = UnitOfWorkFactory.Create())
            {
                _unitOfWork = uow as SQLUnitOfWork;

                using (var cmd = _unitOfWork.CreateCommand())
                {
                    cmd.CommandText = @"SELECT
                                        distinct mail
                                        FROM
                                        SendMailData
                                        where [Status] is null";
                    cmd.Parameters.Clear();

                    var reader = cmd.ExecuteReader();
                    
                    dt.Load(reader);
                }

                _unitOfWork.Dispose();
            }
            return dt;
        }

        public void UpdateStatusSuccess(string mailAccount)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                _unitOfWork = uow as SQLUnitOfWork;

                using (var cmd = _unitOfWork.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE [SendMailData]
                                        SET [Status] = 'success'
                                        WHERE [mail] = @mailAccount
                                            ";

                    var paramMail = cmd.CreateParameter();
                    paramMail.ParameterName = "@mailAccount";
                    paramMail.Value = mailAccount;
                    cmd.Parameters.Add(paramMail);

                    cmd.ExecuteNonQuery();
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Dispose();
            }
        }

        internal void aaaa()
        {
            throw new NotImplementedException();
        }
    }
}
