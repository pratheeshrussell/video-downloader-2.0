using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebVideoDownloader
{
    class YouTubeGenerateSig
    {
        private string Key ;
        private string SigGenerationKey;
        public YouTubeGenerateSig(string Sig, string key2)
        {
            Key = Sig;
            SigGenerationKey = key2;
        }
        public string GenerateSig()
        {
          
            string[] GenKey = SigGenerationKey.Split(',');
            string[] sigArray = CreateArrayFunct(Key);
            int temp =0;
            //"EX4,SE5,R"
            //EX --> exchanges item with item at index 0
            //SE --> removes so much items from index 0
            //R --> reverses the array
            foreach (string elem in GenKey)
            {
                if (elem.ToLower().Contains("ex"))
                {
                    temp = Int32.Parse(elem.ToLower().Replace("ex",""));
                    sigArray = ExchangeArrayFunct(sigArray, temp);
                }
                if (elem.ToLower().Contains("se"))
                {
                    string[] temp2 = sigArray;
                    temp = Int32.Parse(elem.ToLower().Replace("se", ""));
                    sigArray = new string[sigArray.Length -temp];
                    sigArray = SpliceArrayFunct(temp2, temp);
                }
                if (elem.ToLower().Contains("r"))
                {
                    sigArray = ReverseArrayFunct(sigArray);
                }
            }
            string output = string.Join("", sigArray);
            return (output);
        }
        //create the array
        private string[] CreateArrayFunct(string input)
        {
            string[] output = new string[input.Length];
            int j = 0;
            foreach(char i in input)
            {
                output[j] = i.ToString();
                j += 1;
            }
            return output;
        }

        // the reverse,exchange and splice functions
        private string[] ReverseArrayFunct(string[] input)
        {
            Array.Reverse(input);
            string[] output = input;
            return output;
        }
        private string[] ExchangeArrayFunct(string[] input, int b)
        {
            string c = input[0].ToString();
            int d = b % input.Length;
            input[0] = input[d];
            input[d] = c;
            string[] output = input;
            return output;
        }
        private string[] SpliceArrayFunct(string[] input, int b)
        {
            int n = 0;
            int m = 0;
            string[] output = new string[input.Length - b];
            foreach (string i in input)
            {
                if (n > b-1){
                    output[m] = i;
                    m += 1;
                }
                n += 1;
            }
            return output;
        }

    }
}
