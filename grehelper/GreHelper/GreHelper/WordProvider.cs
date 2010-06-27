using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.IO.Log;

namespace GreHelper
{
    public class ChoiceRecord
    {
        public string Word;
        public string Definition;
        // This will also contain the definition. This prepares all the choices, correct + incorrect.
        public string[] Options;
        public int CorrectChoiceIndex;
    }

    public class WordProvider
    {
        private const int MAXCHOICES = 4;
        private static Logger logger;
        private Dictionary<string, string> localDictionary = new Dictionary<string, string>();
        
        public static WordProvider Create(string filePath, Logger logger)
        {
            WordProvider.logger = logger;
            logger.Write("Creating wordlist from file {0}", filePath);
            WordProvider wp = new WordProvider();
            wp.ReadWordList(File.ReadAllText(filePath));
            return wp;
        }

        public static WordProvider CreateFromResource(string resourceName, Logger logger)
        {
            WordProvider.logger = logger;
            logger.Write("Creating wordlist from resource {0}", resourceName);
            WordProvider wp = new WordProvider();
            wp.ReadWordList(Properties.Resources.ResourceManager.GetString(resourceName));
            return wp;
        }

        public KeyValuePair<string, string> GetRandomWord()
        {
            Random random = new Random();
            var result = localDictionary.ElementAt(random.Next(0, localDictionary.Count));
            logger.Write("Returning Random Word {0}", result.Key);
            return result;
        }

        public ChoiceRecord GetRandomWordWithChoices()
        {
            ChoiceRecord record = new ChoiceRecord();
            var randomWord = GetRandomWord();
            Random random = new Random();
            int correctIndex = random.Next(0, MAXCHOICES - 1);
            record.Word = randomWord.Key;
            record.Definition = randomWord.Value;
            record.CorrectChoiceIndex = correctIndex;
            record.Options = new string[MAXCHOICES];
            record.Options[correctIndex] = randomWord.Value;
            for (int i = 0, currentIndex = 0; i < MAXCHOICES - 1; )
            {
                var w = GetRandomWord();
                if(string.Compare(w.Key, randomWord.Key, true) == 0)
                    continue;
                if (currentIndex == correctIndex)
                    currentIndex++;
                Debug.Assert(currentIndex <= MAXCHOICES - 1);
                record.Options[currentIndex++] = w.Value;
                i++;
            }
            return record;
        }

        private void ReadWordList(string wordListRaw)
        {
            logger.Write("Starting wordlist parse.");
            string[] lines = wordListRaw.Split("\n".ToCharArray());
            int currentLineNumber = 0;
            foreach (var line in lines)
            {
                currentLineNumber++;
                if (string.IsNullOrEmpty(line.Trim()))
                {
                    continue;
                }
                string[] tokens = line.Split("|".ToCharArray(), 2);
                Debug.Assert(logger != null);
                if (tokens.Length < 2)
                {
                    string logMessage = string.Format("Offending record {0} found at line {1}", tokens[0],
                                                      currentLineNumber);
                    logger.Write(logMessage, true);
                    continue;
                }
                localDictionary.Add(tokens[0], tokens[1]);
            }
            logger.Write("Read total {0} words", localDictionary.Keys.Count);
        }
    }
}
