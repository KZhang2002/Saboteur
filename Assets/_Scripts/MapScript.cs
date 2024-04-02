using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public class POI
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public HashSet<string> Adjacent { get; set; }

        public POI(string id, string name, string poiType)
        {
            Id = id;
            Name = name;
            Type = poiType;
            Adjacent = new HashSet<string>();
        }
    }

    public Dictionary<string, POI> POIs;

    public Graph()
    {
        POIs = new Dictionary<string, POI>();
    }

    public void AddNode(string nodeId, string name, string poiType)
    {
        Debug.Log($"{nodeId}: {nodeId}, {name}, {poiType}");
        if (!POIs.ContainsKey(nodeId))
        {
            POI newPoi = new POI(nodeId, name, poiType);
            POIs[nodeId] = newPoi;
        }
        
        foreach (var poi in POIs)
        {
            Debug.Log($"{poi.Key}: {poi.Value.Id}, {poi.Value.Name}, {poi.Value.Type}");
        }
    }

    private void AddEdge(string id1, string id2)
    {
        if (POIs.ContainsKey(id1) && POIs.ContainsKey(id2))
        {
            POIs[id1].Adjacent.Add(id2);
            POIs[id2].Adjacent.Add(id1);
        }
    }
}

public class MapScript : MonoBehaviour
{
    private Graph _gameMap;
    public string[,] unserializable = new string[2, 2];
    [SerializeField] private TextAsset roadData;
    
    private void Start()
    {
        _gameMap = new Graph();
        var poiArr = transform.GetComponentsInChildren<HoverName>();
        foreach (var hoverName in poiArr)
        {
            string poiName = hoverName.ttName;
            string type = hoverName.gameObject.transform.parent.name;
            _gameMap.AddNode(hoverName.name, poiName, type);
        }

        string roadText = roadData.text;
        string[] lines = roadText.Split('\n');

        foreach (var line in lines)
        {
            string[] pois = line.Split(',');

            int i = 0;
            string start = null;
            foreach (string poi in pois)
            {
                if (i == 0)
                {
                    start = poi;
                }
                else
                {
                    if (start == null)
                    {
                        Debug.Log("Invalid POI.");
                        i++;
                        continue;
                    }
                    _gameMap.POIs[start].Adjacent.Add(poi);
                    Debug.Log($"Added {poi} to {start}");
                }
                
                i++;
            }
        }
    }

    private void ReadCsv()
    {
        
    }

    private void Update()
    {

    }
}
