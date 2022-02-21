using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using BusinessLayer;

namespace DataLayer
{
    public class UserServices : IUserServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public UserServices(TCContext context)
        {
            this.context = context;
        }

        //Autocomplete By Name
        public List<Users> AutoCompleteName(string Name)
        {
            List<Users> list = new List<Users>();
            list = (from p in context.usermasters
                    where p.Name.Contains(Name)
                    group p by p.Name into g
                    select new Users
                    {
                        Name = g.Key
                    }
                    ).ToList();
            return list;
        }

        //Autocomplete By Army No
        public List<Users> AutoCompleteArmyNo(string ArmyNo)
        {
            List<Users> list = new List<Users>();
            list = (from p in context.usermasters
                    where p.ArmyNumber.Contains(ArmyNo)
                    group p by p.ArmyNumber into g
                    select new Users
                    {
                        ArmyNumber = g.Key
                    }
                    ).ToList();
            return list;
        }

        //Autocomplete By Icard
        public List<Users> AutoCompleteIcard(string ICard)
        {
            List<Users> list = new List<Users>();
            list = (from p in context.usermasters
                    where p.IDCardNo.Contains(ICard)
                    group p by p.IDCardNo into g
                    select new Users
                    {
                        IDCardNo = g.Key,
                    }
                    ).ToList();
            return list;
        }


        //check password MD5 Encrypted
        public int CheckPassword(string password, Int64 id)
        {
            var checkpassword = context.usermasters.FirstOrDefault(p => p.Password == password && p.ID == id);
            if (checkpassword != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //get Users by ICARD and group By IDCard
        public List<Users> PaggingByIDCardGroupByICard(Int32 take, Int32 skip)
        {
            List<Users> list = new List<Users>();

            list = (from p in context.usermasters
                    group p by p.ArmyNumber into g
                    select new Users
                    {
                        ArmyNumber = g.Key
                    }).OrderBy(o => o.ArmyNumber).Skip(skip).Take(take).ToList();

            return list;
        }

        //get Users by ICARD 
        public List<Users> PaggingByIDCard(Int32 take, Int32 skip, string icard)
        {
            List<Users> list = new List<Users>();

            list = (from p in context.usermasters
                    join a in context.admasters on p.ADID equals a.ID
                    join r in context.rankmasters on a.RankID equals r.ID
                    join h in context.hqmasters on a.HQID equals h.ID
                    join c in context.categorymasters on a.CategoryID equals c.ID
                    join d in context.divmasters on a.DivID equals d.ID
                    join u in context.unitmasters on a.UnitID equals u.ID
                    where p.ArmyNumber == icard
                    select new Users
                    {
                        ID = p.ID,
                        IDCardNo = p.IDCardNo,
                        Name = p.Name,
                        ArmyNumber = p.ArmyNumber,
                        Rank = r.Rank,
                        DIVName = d.DivName,
                        HQName = h.HQName,
                        CategoryName = c.CategoryName,
                        UnitName = u.UnitName,
                        CreatedOn = p.CreatedOn,
                        ADNO = a.ADNO,
                        IsManifest = a.IsManifest,
                        ADID = a.ID,
                        Session = a.Session
                    }).ToList();

            return list;
        }

        //get user by id card
        public Users GetUserByICard(string Icard)
        {
            Users users = new Users();
            var user = (from u in context.usermasters
                        join a in context.admasters on u.ADID equals a.ID
                        join un in context.unitmasters on a.UnitID equals un.ID
                        where u.IDCardNo == Icard
                        orderby u.ID descending
                        select new Users
                        {
                            Name = u.Name,
                            ArmyNumber = u.ArmyNumber,
                            IDCardNo = u.IDCardNo,
                            UnitID = u.UnitID,
                            DivID = un.DivID,
                            RankID = u.RankID,
                            HQID = un.HQID,
                            CategoryID = u.CategoryID,
                            CityID = un.CityID,
                            BrigadeID = a.BrigadeId
                        }).FirstOrDefault();
            return user;
        }

        //get user by id card
        public Users GetUserByArmyNo(string armyno)
        {
            Users users = new Users();
            var user = (from u in context.usermasters
                        join a in context.admasters on u.ADID equals a.ID
                        join un in context.unitmasters on u.UnitID equals un.ID
                        where u.ArmyNumber == armyno
                        orderby u.ID descending
                        select new Users
                        {
                            Name = u.Name,
                            ArmyNumber = u.ArmyNumber,
                            IDCardNo = u.IDCardNo,
                            UnitID = u.UnitID,
                            DivID = un.DivID,
                            RankID = u.RankID,
                            HQID = un.HQID,
                            CategoryID = u.CategoryID,
                            CityID = un.CityID,
                            ADNO = a.ADNO,
                        }).FirstOrDefault();
            return user;
        }

        //get user by id
        public Users GetUserByID(Int64 id)
        {
            Users users = new Users();
            var user = (from u in context.usermasters
                        where u.ID == id
                        select new Users
                        {
                            Name = u.Name,
                            ArmyNumber = u.ArmyNumber,
                            IDCardNo = u.IDCardNo,
                            Regiment = u.Regiment,
                            UserName = u.UserName,
                            Password = u.Password,
                            PasswordSalt = u.PasswordSalt

                        }).FirstOrDefault();
            return user;
        }

        //check if user exist
        public int UserExist(string username)
        {
            var userid = context.usermasters.FirstOrDefault(u => u.UserName == username)?.ID;
            if (userid == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(userid);
            }
        }

        //Insert Users
        public Int64 Insert(Users user)
        {
            var usermaster = new usermaster
            {
                IDCardNo = user.IDCardNo,
                ArmyNumber = user.ArmyNumber,
                Name = user.Name,
                RankID = user.RankID,
                UnitID = user.UnitID,
                DivID = user.DivID,
                HQID = user.HQID,
                ADID = user.ADID,
                CategoryID = user.CategoryID,
                CreatedOn = user.CreatedOn
            };
            context.usermasters.Add(usermaster);
            return usermaster.ID;
        }

        //update user
        public Int64 Update(Users user)
        {
            var data = (from u in context.usermasters
                        where u.ADID == user.ADID
                        select u).FirstOrDefault();
            data.ArmyNumber = user.ArmyNumber;
            data.Name = user.Name;
            data.IDCardNo = user.IDCardNo;
            data.RankID = user.RankID;
            data.UnitID = user.UnitID;
            data.DivID = user.DivID;
            data.HQID = user.HQID;
            data.CategoryID = user.CategoryID;
            data.UpdatedOn = user.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of users from database
        public List<Users> Paging(Int32 take, Int32 skip)
        {
            List<Users> list = new List<Users>();
            try
            {
                list = (from u in context.usermasters
                        join r in context.rankmasters on u.RankID equals r.ID
                        select new Users
                        {
                            ID = u.ID,
                            IDCardNo = u.IDCardNo,
                            ArmyNumber = u.ArmyNumber,
                            Name = u.Name,
                            UserName = u.UserName,
                            Rank = r.Rank,
                            Regiment = u.Regiment,
                        }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            }
            catch (Exception ex)
            {

            }

            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.usermasters
                         select p).Count();

            return count;
        }
        public Int32 TotalItemsUserHistory()
        {

            var count = (from p in context.usermasters
                         group p by p.ArmyNumber into g
                         select new Users
                         {
                             ArmyNumber = g.Key
                         }).Count();

            return count;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItemsUserHistory(string armyno)
        {
            var count = (from p in context.usermasters
                         where p.ArmyNumber == armyno
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from U in context.usermasters
                        where U.ID == ID
                        select U).FirstOrDefault();
            context.usermasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list of users from database
        public List<Users> GetSearchResult(String SearchText, int searchtype)
        {
            List<Users> list = new List<Users>();
            if (searchtype == 1)
            {
                list = (from u in context.usermasters
                        join r in context.rankmasters on u.ID equals r.ID
                        where u.Name.Contains(SearchText)
                        select new Users
                        {
                            ID = u.ID,
                            IDCardNo = u.IDCardNo,
                            ArmyNumber = u.ArmyNumber,
                            Name = u.Name,
                            UserName = u.UserName,
                            Rank = r.Rank,
                            Regiment = u.Regiment

                        }).OrderByDescending(x => x.ID).ToList();
                return list;
            }
            else if (searchtype == 2)
            {
                list = (from u in context.usermasters
                        join r in context.rankmasters on u.ID equals r.ID
                        where u.ArmyNumber.Contains(SearchText)
                        select new Users
                        {
                            ID = u.ID,
                            IDCardNo = u.IDCardNo,
                            ArmyNumber = u.ArmyNumber,
                            Name = u.Name,
                            UserName = u.UserName,
                            Rank = r.Rank,
                            Regiment = u.Regiment

                        }).OrderByDescending(x => x.ID).ToList();
                return list;
            }
            else if (searchtype == 3)
            {
                list = (from u in context.usermasters
                        join r in context.rankmasters on u.ID equals r.ID
                        where u.IDCardNo.Contains(SearchText)
                        select new Users
                        {
                            ID = u.ID,
                            IDCardNo = u.IDCardNo,
                            ArmyNumber = u.ArmyNumber,
                            Name = u.Name,
                            UserName = u.UserName,
                            Rank = r.Rank,
                            Regiment = u.Regiment

                        }).OrderByDescending(x => x.ID).ToList();
                return list;
            }
            else
            {
                list = (from u in context.usermasters
                        join r in context.rankmasters on u.ID equals r.ID
                        select new Users
                        {
                            ID = u.ID,
                            IDCardNo = u.IDCardNo,
                            ArmyNumber = u.ArmyNumber,
                            Name = u.Name,
                            UserName = u.UserName,
                            Rank = r.Rank,
                            Regiment = u.Regiment

                        }).OrderByDescending(x => x.ID).ToList();
                return list;
            }
        }

        //Get searched list of users from database by searchtext
        public List<Users> GetSearchResultBySearchText(String SearchText)
        {
            List<Users> list = new List<Users>();

            list = (from p in context.usermasters
                    where p.ArmyNumber.Contains(SearchText) || p.IDCardNo.Contains(SearchText)
                    group p by p.ArmyNumber into g
                    select new Users
                    {
                        ArmyNumber = g.Key
                    }).ToList();
            return list;
        }

        //save context
        public void Save()
        {
            context.SaveChanges();
        }

        #endregion

        #region IDisposable Interface Implementation
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
