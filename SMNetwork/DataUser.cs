﻿using System;

namespace SMNetwork
{
    [Serializable]
    public class DataUser
    {
        public string Login { get; set; }
        
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        public string Description { get; set; }  
        
        public string ID { get; set; }
        
        public DataUser()
        {
        }

        public override string ToString()
        {
            return "ID: " + ID + "; Login; " + Login + ";Lastname: " + Lastname + "; Firstname: " + Firstname + "; Description: " +
                   Description;
        }
    }
}