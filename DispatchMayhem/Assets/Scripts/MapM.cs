namespace Mapbox.Examples
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.MeshGeneration.Factories;
    using Mapbox.Unity.Utilities;


    public class MapM : MonoBehaviour
    {
        private static MapM instance = null;
        public static MapM inst { get { return instance; } }

        [SerializeField]
        AbstractMap _map;

        [SerializeField]
        [Geocode]
        string[] _locationStrings;
        Vector2d[] _locations;

        [SerializeField]
        float _spawnScale = 3.0f;

        //[SerializeField]
        //GameObject _truckPrefab;
        //[SerializeField]
        //GameObject _trailerPrefab;

        List<GameObject> _spawnedObjects;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this.gameObject);
            }

            _spawnedObjects = new List<GameObject>();
            _spawnedObjects.Clear();
        }

        private void OnApplicationQuit()
        {
            instance = null;
        }

        void Start()
        {
            //_map = this.gameObject.GetComponent<AbstractMap>();
            _locations = new Vector2d[_locationStrings.Length];

            for (int i = 0; i < _locationStrings.Length; i++)
            {
                var locationString = _locationStrings[i];
                _locations[i] = Conversions.StringToLatLon(locationString);
            }
        }

        private void Update()
        {
            int count = _spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _spawnedObjects[i];
                //var location = _locations[i];
                Vector2d loc = Vec2To2d(spawnedObject.GetComponent<MapSupport>().gps);
                if (spawnedObject.tag == "City") loc = loc + new Vector2d(-3.20f, 0.15f);

                //spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                Vector2d tloc = new Vector2d(loc.y, loc.x);
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(tloc, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                //Debug.Log(spawnedObject.transform.localPosition);
            }
        }

        /******************************************************************************************
                AddToMap

                This method takes a game object and adds it to map. Not that the game object must 
                have an attached MapSupport script

         *******************************************************************************************/
        public bool AddToMap(GameObject go)
        {
            MapSupport ms = go.GetComponent<MapSupport>();
            bool retval = true;

            if (ms == null)
            {
                Debug.Log("Could not add " + go.name + " to the map");
                retval = false;
            }
            else
            {
                _spawnedObjects.Add(go);
                Debug.Log("Added to Map! " + ms.gps);
                Debug.Log(go.name + " added to the map");
                go.transform.localPosition = _map.GeoToWorldPosition(Vec2To2d(ms.gps), true);
                go.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }

            return retval;

        }

        /******************************************************************************************
            Vec2To2d

            This method takes a standard unity Vector2 (Unity v2017 and newer) to a Vector2d
            (unity v5)

        *******************************************************************************************/
        public Vector2d Vec2To2d(Vector2 vec2)
        {
            Vector2d tv2d;
            tv2d.x = vec2.x;
            tv2d.y = vec2.y;
            return (tv2d);
        }
    }
}

    