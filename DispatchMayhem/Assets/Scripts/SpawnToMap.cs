namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;

    public class SpawnToMap : MonoBehaviour
    {
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

        void Start()
        {

            _map = this.gameObject.GetComponent<AbstractMap>();
            //_trailerPrefab.transform.parent = _truckPrefab.transform;

            _locations = new Vector2d[_locationStrings.Length];
            _spawnedObjects = new List<GameObject>();
            _spawnedObjects.Clear();

            for (int i = 0; i < _locationStrings.Length; i++)
            {
                var locationString = _locationStrings[i];
                _locations[i] = Conversions.StringToLatLon(locationString);
                //var instance = Instantiate(_truckPrefab);
                //var instance_02 = Instantiate(_trailerPrefab);
                //instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
                //instance_02.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
                //instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                //instance_02.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                //_spawnedObjects.Add(instance);
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
                //spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(loc, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }
        /***********************************************************************************
            UpdateMyMapPosition

            This method is used by the game assets to place themselves to the at their current
            GPS locations.

            Not that as public components, the GPSs coordinates could be retrieved from the
            update method and the position can be automatic once the object is added
            to the _spawnedObjects list

        *************************************************************************************/
        //public void UpdateMyMapPosition(GameObject go, Vector2 loc)
        //{
        //    int idx = _spawnedObjects.FindIndex(go);
        //    if (idx != -1 )
        //    {
        //        _locations[idx] = Vec2To2d(loc);
        //        _locations[idx].x = loc.x;
        //        _locations[idx].y = loc.y;
        //    }
        //    else
        //    {
        //        _spawnedObjects.Add(go);
        //        Vector2d tmpvec = Vec2To2d(loc);
        //        _locations.Add(tmpvec);
        //    }
        //}

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
 