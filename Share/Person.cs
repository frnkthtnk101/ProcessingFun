﻿using System;

namespace Share
{
    [Serializable]
    public class Person
    {
        private string _name;
        private int _age;

        public Person(string name, int age){
            _name = name;
            _age = age;
        }
    }
}
