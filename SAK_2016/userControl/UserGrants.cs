using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NormaMeasure.SAC_APP
{
    class UserGrants
    {
        string role = "undefined";
        public  UserGrants(string usrRole)
        {
            this.role = usrRole;
        }

        /// <summary>
        /// Может ли пользователь смотреть таблицу пользователей
        /// </summary>
        /// <returns></returns>
        public bool userCouldSeeUserDb() //может ли видеть БД Пользователей
        {
            return (role == "Администратор БД" || role == "Метролог");
        }
        /// <summary>
        /// Может ли пользователь смотреть таблицу испытаний
        /// </summary>
        /// <returns></returns>
        public bool userCouldSeeTestDb() //может ли видеть БД Испытаний
        {
            return (role == "Администратор БД" || role == "Метролог");
        }
        /// <summary>
        /// Может ли пользователь смотреть таблицу кабелей
        /// </summary>
        /// <returns></returns>
        public bool userCouldSeeCablesDb() //может ли видеть БД Кабелей
        {
            return (role == "Администратор БД" || role == "Метролог");
        }
        /// <summary>
        /// Может ли пользователь смотреть таблицу типов барабанов
        /// </summary>
        /// <returns></returns>
        public bool userCouldSeeBarabansDb() //может ли видеть БД Барабанов
        {
            return (role == "Администратор БД" || role == "Метролог");
        }

        /// <summary>
        /// Может ли пользователь добавлять кабель
        /// </summary>
        /// <returns></returns>
        public bool userCouldAddCable() //может ли добавлять кабель
        {
            return (role == "Администратор БД" || role == "Метролог");
        }

        /// <summary>
        /// Может ли пользователь осуществлять миграцию данных из старых БД
        /// </summary>
        /// <returns></returns>
        public bool userCouldMigrateDataFromOldDB()
        {
            return (role == "Администратор БД");
        }
    }
}
