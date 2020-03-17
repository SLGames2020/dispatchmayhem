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
                MapSupport msup = spawnedObject.GetComponent<MapSupport>();
                Vector2d loc = Vec2To2d(msup.gps);
                if (spawnedObject.tag == "City") loc = loc + new Vector2d(-3.20f, 0.15f); //shift over the cities because they are positioned improperly for some reason

                Vector2d tloc = new Vector2d(loc.y, loc.x);
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(tloc, true);
                spawnedObject.transform.localScale = new Vector3(msup.baseScale.x*_map.Zoom, msup.baseScale.y*_map.Zoom, msup.baseScale.z *_map.Zoom);
            }

            //---------------------------------------------------------------------------------//
            //                               MAP CONTROLS                                      //
            //---------------------------------------------------------------------------------//

            if (Input.GetAxis("Mouse ScrollWheel") > 0.0f) // zoom in
            {
                _map.UpdateMap(_map.AbsoluteZoom + 1.0f);
                if (_map.AbsoluteZoom >= 12.0f)
                {
                    _map.UpdateMap(12.0f);
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0.0f) // zoom out
            {
                _map.UpdateMap(_map.AbsoluteZoom - 1.0f);
                if (_map.AbsoluteZoom <= 8.0f)
                {
                    _map.UpdateMap(8.0f);
                }
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

    