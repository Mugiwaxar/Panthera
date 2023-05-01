using Panthera.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace Panthera.Utils
{
    internal class PantheraSaveSystem
    {

        public static PantheraSaveSystem Instance;

        public static string saveDir = System.IO.Path.Combine(Application.persistentDataPath, "Panthera");
        public static string saveFileName;

        public static Dictionary<string, string> savedData = new Dictionary<string, string>();

        public static void Init()
        {

            // Create the Dir //
            try
            {
                Directory.CreateDirectory(saveDir);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Failed to create Panthera Dir");
                Debug.LogError(e);
            }

        }

        public static void Save()
        {

            // Create the full path //
            string fullFilePath = System.IO.Path.Combine(saveDir, saveFileName + "--Data");

            // Create the XML //
            XElement root = new XElement("Root", from keyValue in savedData select new XElement(keyValue.Key, keyValue.Value));

            // Create the File //
            try
            {
                root.Save(fullFilePath, SaveOptions.OmitDuplicateNamespaces);
            }
            catch (Exception e)
            {
                Debug.LogError("[Panthera] Unable to save ...");
                Debug.LogError(e);
            }

        }

        public static void Load()
        {
            
            // Create the full path //
            string fullFilePath = System.IO.Path.Combine(saveDir, saveFileName + "--Data");

            // Empty the List //
            savedData.Clear();

            // Check if the File exist //
            if (File.Exists(fullFilePath) == false)
            {
                Save();
            }

            // Load the File //
            string file = null;
            try
            {
                file = File.ReadAllText(fullFilePath);
            }
            catch (Exception e)
            {
                Debug.LogError("[Panthera] Unable to load ...");
                Debug.LogError(e);
                return;
            }

            // Create the Dictionary //
            savedData = XElement.Parse(file).Elements().ToDictionary(k => k.Name.ToString(), v => v.Value.ToString());

        }

        public static void SetValue(string key, string value)
        {
            if (savedData.ContainsKey(key) == true) savedData[key] = value;
            else savedData.Add(key, value);
        }

        public static string ReadValue(string key)
        {
            if (savedData.ContainsKey(key) == true) return savedData[key];
            return null;
        }

        public static Dictionary<string, string> LoadPreset(int ID)
        {
            // Create the full path //
            string fullFilePath = System.IO.Path.Combine(saveDir, saveFileName + "--Preset" + ID.ToString() );

            // Create the List //
            Dictionary<string, string> dataList = new Dictionary<string, string>();

            // Check if the File exist //
            if (File.Exists(fullFilePath) == false)
            {
                return null;
            }

            // Load the File //
            string file = null;
            try
            {
                file = File.ReadAllText(fullFilePath);
            }
            catch (Exception e)
            {
                Debug.LogError("[Panthera] Unable to load preset " + ID.ToString() + " ...");
                Debug.LogError(e);
                return null;
            }

            // Create the Dictionary //
            dataList = XElement.Parse(file).Elements().ToDictionary(k => k.Name.ToString(), v => v.Value.ToString());

            return dataList;
        }

        public static void SavePreset(int ID, Dictionary<string, string> dataList)
        {
            // Create the full path //
            string fullFilePath = System.IO.Path.Combine(saveDir, saveFileName + "--Preset" + ID.ToString());

            // Create the XML //
            XElement root = new XElement("Root", from keyValue in dataList select new XElement(keyValue.Key, keyValue.Value));

            // Create the File //
            try
            {
                root.Save(fullFilePath, SaveOptions.OmitDuplicateNamespaces);
            }
            catch (Exception e)
            {
                Debug.LogError("[Panthera] Unable to save preset + " + ID.ToString() + " ...");
                Debug.LogError(e);
            }
        }

    }
}
