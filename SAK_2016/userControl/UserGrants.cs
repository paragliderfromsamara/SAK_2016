using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAK_2016
{
    class UserGrants
    {
        string role = "undefined";
        public  UserGrants(string usrRole)
        {
            this.role = usrRole;
        }

        public bool userCouldSeeUserDb() //может ли видеть БД Пользователей
        {
            return (role == "Администратор БД" || role == "Метролог");
        }
        public bool userCouldSeeTestDb() //может ли видеть БД Испытаний
        {
            return (role == "Администратор БД" || role == "Метролог");
        }
        public bool userCouldSeeCablesDb() //может ли видеть БД Кабелей
        {
            return (role == "Администратор БД" || role == "Метролог");
        }
        public bool userCouldSeeBarabansDb() //может ли видеть БД Барабанов
        {
            return (role == "Администратор БД" || role == "Метролог");
        }

        public bool userCouldAddCable() //может ли добавлять кабель
        {
            return (role == "Администратор БД" || role == "Метролог");
        }
    }
}
