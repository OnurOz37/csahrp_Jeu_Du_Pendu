using System;
using System.Collections.Generic;
using System.IO;
using Jeu_Du_Penduu;

namespace Jeu_Du_Pendu
{
    class Program
    {
        static void Main(string[] args)
        {

            var mots = ChargerLesMots("mots.txt");
            if ((mots == null) || (mots.Length == 0))
            {
                Console.WriteLine("La liste de mots est vide !");
            }
            else
            {
                bool jouer = true; 
                while (jouer)
                {
                    Random motsAl = new Random();
                    int i = motsAl.Next(mots.Length);
                    string mot = mots[i].Trim().ToUpper();


                    DevinerMot(mot);
                    if (!DemanderDeRejouer())
                    {
                        break; 
                    }
                    Console.Clear(); 

                }
                Console.WriteLine("Merci et à bientôt ! ");



            }



        }
        static void DevinerMot(string mot)
        {
            var lettresDevinees = new List<char>();
            var lettresExclues = new List<char>();

            const int NB_VIES = 6;
            int viesRestantes = NB_VIES;
            while (viesRestantes > 0)
            {
                Console.WriteLine(Ascii.PENDU[NB_VIES - viesRestantes]);
                Console.WriteLine();
                AfficherMot(mot, lettresDevinees);
                Console.WriteLine();
                Console.WriteLine();
                var lettre = DemanderUneLettre();
                Console.Clear();
                if (mot.Contains(lettre))
                {
                    Console.WriteLine("Cette lettre est dans le mot ! \nVies restantes : " + viesRestantes);
                    lettresDevinees.Add(lettre);
                    if (ToutesLettresDevinees(mot, lettresDevinees))
                    {
                        break;
                    }
                }
                else
                {
                    if (!lettresExclues.Contains(lettre))
                    {
                        viesRestantes--;
                        lettresExclues.Add(lettre);
                    }


                    Console.WriteLine("Vies restantes : " + viesRestantes);
                }
                if (lettresExclues.Count > 0)
                {
                    Console.WriteLine("Pas de bol ! Le mot ne contient pas les lettres : " + String.Join(", ", lettresExclues));
                    Console.WriteLine();
                }
            }
            Console.WriteLine(Ascii.PENDU[NB_VIES - viesRestantes]);
            if (viesRestantes == 0)
            {
                Console.WriteLine("PERDU ! Le mot était : " + mot);
            }
            else
            {

                AfficherMot(mot, lettresDevinees);
                Console.WriteLine();
                Console.WriteLine("GAGNE ! BRAVO ! ");
            }


        }
        static bool DemanderDeRejouer()
        {
            
            char reponse = DemanderUneLettre("Voulez vous rejouer ? (O/N) :");
            if ((reponse == 'o') || (reponse == 'O'))
            {
                return true; 
            }
            else if ((reponse == 'n') || (reponse == 'N'))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Erreur : Vous devez répondre avec o ou n");
                return DemanderDeRejouer(); 
            }

        }
        static void AfficherMot(string mot, List<char> lettres)
        {

            for (int i = 0; i < mot.Length; i++)
            {
                char lettre = mot[i];
                if (lettres.Contains(lettre))
                {
                    Console.Write(lettre + " ");

                }
                else
                {
                    Console.Write("_ ");

                }
            }

        }
        static char DemanderUneLettre(string message = "Rentrez une lettre : ")
        {

            while (true)
            {
                Console.Write(message);
                string lettreRentree = Console.ReadLine();


                if (lettreRentree.Length == 1)
                {
                    lettreRentree = lettreRentree.ToUpper();
                   // Console.WriteLine("La lettre rentrée est : " + lettreRentree);
                    return lettreRentree[0];
                }
                Console.WriteLine("Erreur: Veuillez rentrer une seule lettre");
            }







        }
        static bool ToutesLettresDevinees(string mot, List<char> lettres)
        {
            foreach (var lettre in lettres)
            {
                mot = mot.Replace(lettre.ToString(), "");
            }
            if (mot.Length == 0)
            {
                return true;
            }
            return false;
        }
        static string[] ChargerLesMots(string nomFichier)
        {
            try
            {
                return File.ReadAllLines(nomFichier);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur du lecture du fichier : " + nomFichier + " (" + ex.Message + ") ");

            }
            return null;

        }


    }
}
