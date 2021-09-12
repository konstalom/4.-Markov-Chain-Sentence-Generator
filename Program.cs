using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Globalization;

namespace _4._Markov_Chain_Sentence_Generator
{
class Program {
    static string[] WordsDevider(string text) {
        string[] separators = new string[] {" ", "\n", "—", "\r", "," , "!", "?", ".", "(", ")", ":", ";"};
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        for(int i = 0; i < words.Length; i++){
            words[i] = words[i].ToLower();
        }
        return words;
    }
    static List<GraphedWord> WordLibraryMaker(string path) {
        List<GraphedWord> listofwords = new List<GraphedWord>();
        string[] words;
        StreamReader sr = new StreamReader(path);
        words = WordsDevider(sr.ReadToEnd());
        foreach (string word in words) {
            int indexofword = Array.IndexOf(words, word);
            if (listofwords.Exists(element => element.Word == word)) {//if this element already exists
                if (Array.Exists<string>(words, element => indexofword + 1 == Array.IndexOf(words, element))) {//if that is not the last word
                    listofwords.Find(element => element.Word == word).NextWords.SpeciallyAdd(words[indexofword + 1]);
                }
                else {//if that is the last
                    listofwords.Find(element => element.Word == word).NextWords.SpeciallyAdd(word);
                    }
            }
            else {//if he not exists
                if (Array.Exists<string>(words, element => indexofword + 1 == Array.IndexOf(words, element))) {//if that is not the last word
                    listofwords.Add(new GraphedWord(word, new NextWord(words[indexofword + 1])));
                    }
                else {//if that is the last
                    listofwords.Add(new GraphedWord(word));
                }
            }
            words[indexofword] = null;
        }
        return listofwords;
    }
    static string RandomSign(){//usually does not work
        Random rnd = new Random();
        int choose = rnd.Next(0, 5);
        switch(choose){
            case 0: return "!";
            case 1: return ".";
            case 2: return "?";
            case 3: return "...";
            case 4: return ")))";
            case 5: return "(((";
            default: return ".";
        }
    }
    static void SentenceGenerator(List<GraphedWord> listofwords, int sentences, int numberofwords) {
        Random rnd = new Random();
        GraphedWord word = new GraphedWord() { };
        for (int i = 0; i < sentences; i++){
            string sentence = "";
            word = listofwords[rnd.Next(0, listofwords.Count() - 1)];
            sentence += word.Word;
            for(int k = 0; k + 1 < numberofwords; k++){
                string nextwordsaved = word.NextWord();
                sentence += " " + nextwordsaved;
                word = listofwords.Find(element => element.Word == nextwordsaved);
            }
            Console.WriteLine(char.ToUpper(sentence[0]) + sentence.Substring(1) + RandomSign());
        }
    }
    static void MyMain(){
            List<GraphedWord> listofwords = new List<GraphedWord>();
            bool x = true;
            int sentences = 0;
            int numberofwords = 0;
            Console.WriteLine("Hello! Type an origin .txt file path. (As big as possible):");
            while (x == true)
            {
                try
                {
                    listofwords = WordLibraryMaker(Console.ReadLine());
                    x = false;
                }
                catch
                {
                    Console.WriteLine("Wrong path! Try again:");
                }
            }
            Console.WriteLine("Now choose: 1. Number of sentences; 2. Number of words in each of them:");
            x = true;
            while (x == true)
            {
                try
                {
                    sentences = Convert.ToInt32(Console.ReadLine());
                    x = false;
                }
                catch
                {
                    Console.WriteLine("Type number of sentences again:");
                }
            }
            x = true;
            while (x == true)
            {
                try
                {
                    numberofwords = Convert.ToInt32(Console.ReadLine());
                    x = false;
                }
                catch
                {
                    Console.WriteLine("Type number of words again:");
                }
            }
            SentenceGenerator(listofwords, sentences, numberofwords);
        }
    
    static void Main(string[] args) {
        MyMain();
    }
}
}
    
