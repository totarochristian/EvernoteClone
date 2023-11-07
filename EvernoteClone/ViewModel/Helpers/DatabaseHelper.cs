﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteClone.ViewModel.Helpers
{
    class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        /// <summary>
        /// Method that will insert a generic type of object in a generic table of the database.
        /// </summary>
        /// <typeparam name="T">Type of the object to be inserted in the database</typeparam>
        /// <param name="item">Object to be inserted in the database</param>
        /// <returns></returns>
        public static bool Insert<T>(T item)
        {
            bool result = false;

            //Connect to the database
            using(SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                //Create a generic table in the database, if not exists
                conn.CreateTable<T>();
                //Insert the item in the database and save the rows affected
                int rows = conn.Insert(item);
                //If the number of rows affected is greater then 0, set result to true, otherwise false
                result = rows > 0;
            }

            return result;
        }

        /// <summary>
        /// Method that will update a generic type of object in a generic table of the database.
        /// </summary>
        /// <typeparam name="T">Type of the object to be updated in the database</typeparam>
        /// <param name="item">Object to be updated in the database</param>
        /// <returns></returns>
        public static bool Update<T>(T item)
        {
            bool result = false;

            //Connect to the database
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                //Create a generic table in the database, if not exists
                conn.CreateTable<T>();
                //Update the item in the database and save the rows affected
                int rows = conn.Update(item);
                //If the number of rows affected is greater then 0, set result to true, otherwise false
                result = rows > 0;
            }

            return result;
        }

        /// <summary>
        /// Method that will delete a generic type of object in a generic table of the database.
        /// </summary>
        /// <typeparam name="T">Type of the object to be deleted in the database</typeparam>
        /// <param name="item">Object to be deleted in the database</param>
        /// <returns></returns>
        public static bool Delete<T>(T item)
        {
            bool result = false;

            //Connect to the database
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                //Create a generic table in the database, if not exists
                conn.CreateTable<T>();
                //Delete the item in the database and save the rows affected
                int rows = conn.Delete(item);
                //If the number of rows affected is greater then 0, set result to true, otherwise false
                result = rows > 0;
            }

            return result;
        }

        /// <summary>
        /// Method that will read a list of objects of a generic type from a generic table of the database.
        /// </summary>
        /// <typeparam name="T">Type of the objects to be readed in the database</typeparam>
        /// <returns>List with a generic type of object</returns>
        public static List<T> Read<T>() where T : new()
        {
            List<T> items;

            //Connect to the database
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                //Create a generic table in the database, if not exists
                conn.CreateTable<T>();
                //Retrieve the content of the generic table in a querable interface, convert it to a list and save in items
                items = conn.Table<T>().ToList();
            }

            return items;
        }
    }
}
