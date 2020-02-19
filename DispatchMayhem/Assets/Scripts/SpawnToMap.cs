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

		[SerializeField]
		GameObject _truckPrefab;
        [SerializeField]
        GameObject _trailerPrefab;

        GameObject _truckSpawn;
        GameObject _trailerSpawn;

        List<GameObject> _spawnedObjects;

		void Start()
		{
            _truckSpawn = Instantiate(_truckPrefab);
            _trailerSpawn = Instantiate(_trailerPrefab);

            _trailerSpawn.transform.parent = _truckSpawn.transform;

			_locations = new Vector2d[_locationStrings.Length];
			_spawnedObjects = new List<GameObject>();
            _spawnedObjects.Clear();

            for (int i = 0; i < _locationStrings.Length; i++)
			{
				var locationString = _locationStrings[i];
				_locations[i] = Conversions.StringToLatLon(locationString);
				var instance = Instantiate(_truckPrefab);
				var instance_02 = Instantiate(_trailerPrefab);
				instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
				instance_02.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				instance_02.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				_spawnedObjects.Add(instance);
			}
		}

		private void Update()
		{
			int count = _spawnedObjects.Count;
			for (int i = 0; i < count; i++)
			{
				var spawnedObject = _spawnedObjects[i];
				var location = _locations[i];
				spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
				spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			}
		}
	}
}