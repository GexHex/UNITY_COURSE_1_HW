using Assets._Project.Develop.Runtime.Configs.Meta.Level;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.Generators
{
    public class RandomGeneratorService
    {
        public List<char> GenerateRightAnswers(bool isDigit, LevelConfig levelConfig, int generateCount)
        {
            List<char> source = isDigit ? new List<char>(levelConfig.Numbers) : new List<char>(levelConfig.Symbols);

            List<char> generatedRightAnswers = GenerateRandomSymbols(source, generateCount);

            Debug.Log($"Генерация правильного ответа завершена: {new string(generatedRightAnswers.ToArray())}");

            return generatedRightAnswers;
        }

        private List<char> GenerateRandomSymbols(List<char> source, int generateCount)
        {
            List<char> generatedRightAnswers = new();

            while (generatedRightAnswers.Count < generateCount)
            {
                int index = Random.Range(0, source.Count);

                generatedRightAnswers.Add(source[index]);

                source.RemoveAt(index);
            }

            return generatedRightAnswers;
        }
    }
}