using System.Collections.Generic;
using System.IO;
using Serialization;
using UnityEngine;

namespace CubeTower.Common.Data
{
    public class DataManager : IDataManager
    {
        private const string SaveFileName ="Save.json";

        private readonly IDataLoader _dataLoader;
        private readonly Dictionary<string, IData> _dataNodes = new();
        private readonly ISerializer _serializer;
        private readonly List<IData> _datas;
        private DataFull _dataFull;
        private string _savePath;
        
        public DataManager(ISerializer serializer, List<IData> datas)
        {
            _savePath = $"{Application.persistentDataPath}/Data/";
            _serializer = serializer;
            _datas = datas;
            _dataLoader = new FileDataLoader(new JsonFileSerializer());
            
            if (!Directory.Exists(_savePath))
            {
                Directory.CreateDirectory(_savePath);
            }
            
            foreach (var data in _datas)
            {
                if (!_dataNodes.ContainsKey(data.Name()))
                {
                    _dataNodes.Add(data.Name(), data);
                }
                else if (_dataNodes[data.Name()] == null)
                {
                    _dataNodes[data.Name()] = data;
                }
            }
        }

        public IData GetData(string key)
        {
            if (!_dataNodes.ContainsKey(key))
            {
                return null;
            }

            return _dataNodes[key];

        }

        public void Save()
        {
            SaveInternal();
        }

        private void SaveInternal()
        {
            if (_dataNodes != null && _dataNodes.Count > 0)
            {
                var saveData = CreateSaveData();
                
                _dataLoader.Save(saveData, SaveFileName);

            }
            else
            {
                Debug.LogError("DataManager >> Save cannot be done cause data not loaded");
            }
        }
        
        private ObjectRepositoriesContainer CreateSaveData()
        {
            _dataFull = new DataFull();
            
            foreach (var dataNode in _dataNodes)
            {
                var save = _serializer.Write(dataNode.Value);
                
                _dataFull.AddData(new ObjectRepository(dataNode.Key, save));
            }

            return _dataFull.GetContainer();
        }
    }
}