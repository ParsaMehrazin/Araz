//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Models
//{
//    class KoleMoeinRepository : IRepository<Person>
//    {
//        private Araz_Db _dbContext;
//        public void Add(Person entity)
//        {
//            _dbContext.Person.Add(entity);
//            _dbContext.SaveChanges();
//        }

//        public void Delete(Person entity)
//        {
//            var resultForDelete = _dbContext.Tbl_KoleMoein.Where(rows => rows.KmVcCode == entity.KmVcCode).FirstOrDefault();
//            if (resultForDelete != null)
//            {
//                _dbContext.Tbl_KoleMoein.Remove(resultForDelete);
//                _dbContext.SaveChanges();
//            }
//        }

//        public IList<Person> GetAll()
//        {
//            return _dbContext.Tbl_KoleMoein.ToList();
//        }

//        public IList<Person> GetById(int id)
//        {
//            return _dbContext.Tbl_KoleMoein.Where(row => row.KmVcCode == id).ToList();

//        }

//        public void Update(Person entity)
//        {
//            var resualtForUpdate = _dbContext.Tbl_KoleMoein.Where(row => row.KmVcCode == entity.KmVcCode).FirstOrDefault();
//            resualtForUpdate.KmActive = entity.KmActive;
//            resualtForUpdate.KmCodeNumber = entity.KmCodeNumber;
//            // resualtForUpdate.KmIsChild = entity.KmIsChild;
//            //resualtForUpdate.KmParent = entity.KmParent;
//            resualtForUpdate.KmTitle = entity.KmTitle;
//            //resualtForUpdate.MhgVcCode = entity.MhgVcCode;
//            _dbContext.SaveChanges();

//        }
//    }
//}
