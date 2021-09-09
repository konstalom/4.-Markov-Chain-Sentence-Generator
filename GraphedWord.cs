using System;
using System.Collections.Generic;
using System.Linq;

namespace _4._Markov_Chain_Sentence_Generator
{
    class GraphedWord {
        public GraphedWord(string word, NextWord nword) { Word = word; NextWords.SpeciallyAdd(nword.Word); }
        public GraphedWord(string word) { Word = word; NextWords.SpeciallyAdd(Word); }
        public GraphedWord() { }
        private string word;
        public string Word { get; set; }
        private List<NextWord> nextWords = new List<NextWord>();
        public List<NextWord> NextWords { 
            get{
                return nextWords;
            } 
            set{} 
            }
        private int quantityOfConnectings;
        public int QuantityOfConnectings{
            get {
                int q = 0;
                foreach(NextWord nword in NextWords) {
                    q += nword.TimesOccured;
                }
                return q;
            }
        }
        public string NextWord() {
            Random rnd = new Random();
            int[] probabilities = new int[NextWords.Count()];
            for(int k = 0; k < NextWords.Count(); k++) {
                probabilities[k] = NextWords[k].TimesOccured;
            }
            int chosenword = rnd.Next(0, QuantityOfConnectings);
            int count = 0;
            int sum = 0;
            for(int i = 0; chosenword > sum; i++){
                sum += NextWords[i].TimesOccured;
                count++;
            }
            return NextWords[count].Word;
        }
    }
    static class ListNextWordExtention
    {
        public static void SpeciallyAdd(this List<NextWord> list, string word)
        {
            if (list.Exists(element => element.Word == word))
            {//if there is such word already here
                list.Find(element => element.Word == word).TimesOccured++;
            }
            else
            {
                list.Add(new NextWord(word));
            }
        }
    }
    class NextWord : GraphedWord
    {
        public NextWord(string nword) { Word = nword; TimesOccured++; }
        private int timesOccured = 0;
        public int TimesOccured { get; set; }
    }
}
    
