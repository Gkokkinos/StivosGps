void Update()
	{
		switch (state) {
		case LocationState.Enabled:
			if (state == LocationState.Enabled){
			float deltaDistance = Haversine (ref latitude, ref longitude) * 1000;

				distance += deltaDistance;
				distanceText.text = "Distance:" + (distance/1000).ToString("F3") +"km";
				latitudeText.text = "latitude:" + latitude.ToString();
				longtitudeText.text = "lon:" + longitude.ToString();
				stivos = (int)distance/100;
				PlayerPrefs.SetFloat("stivos", stivos);
				PlayerPrefs.SetFloat("kmeters", kmeters +distance);
			}
			break;
		case LocationState.Disabled:
			distanceText.text = "Distance Disabled" ;
			break;
		case LocationState.Failed:
			distanceText.text = "Distance Failed" ;
			break;
		case LocationState.TimedOut:
			distanceText.text = "Distance Timed out" ;
			break;
		}
	}

	float Haversine(ref float lastLatitude, ref float lastLongitude)
	{
		float newLatitude = Input.location.lastData.latitude;
		float newLongitude = Input.location.lastData.longitude;
		float deltaLatitude = (newLatitude - lastLatitude) * Mathf.Deg2Rad;
		float deltaLongitude = (newLongitude - lastLongitude) * Mathf.Deg2Rad;
		float a = Mathf.Pow(Mathf.Sin(deltaLatitude / 2), 2) +
			Mathf.Cos(lastLatitude * Mathf.Deg2Rad) * Mathf.Cos(newLatitude * Mathf.Deg2Rad) *
				Mathf.Pow(Mathf.Sin(deltaLongitude / 2), 2);
		lastLatitude = newLatitude;
		lastLongitude = newLongitude;
		float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
		return EARTH_RADIUS * c;
	}
