﻿using PizzaApp.extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaApp.Model
{
    public class Pizza
    {
        //nom, prix, ingredients
        public string nom { get;set;}
        public string imageUrl { get; set; }
        public int prix { get; set; }
        public string[] ingredients { get; set; }

        //Prix Euros
        //Ingredients string
        public string PrixEuros { get { return prix + "€";  } }

        public string IngerdientsStr { get { return String.Join(", ", ingredients); } }

        public string Titre { get { return nom.PremiereLettreMajuscule(); } }

        public Pizza()
        {

        }
    }
}
