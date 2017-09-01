using GeoLib.Contracts;
using GeoLib.Data;
using System;
using System.Collections.Generic;

namespace GeoLib.Services
{
	public class GeoManager : IGeoService
	{
		private IZipCodeRepository _zipCodeRepository;
		private IStateRepository _stateRepository;

		public GeoManager()
		{
			_zipCodeRepository = new ZipCodeRepository();
			_stateRepository = new StateRepository();
		}

		public GeoManager(IZipCodeRepository zipCodeRepository)
		{
			_zipCodeRepository = zipCodeRepository;
			_stateRepository = new StateRepository();
		}

		public GeoManager(IStateRepository stateRepository)
		{
			_zipCodeRepository = new ZipCodeRepository();
			_stateRepository = stateRepository;
		}

		public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
		{
			_zipCodeRepository = zipCodeRepository;
			_stateRepository = stateRepository;
		}

		public IEnumerable<string> GetStates(bool primaryOnly)
		{
			List<string> stateData = new List<string>();
			IEnumerable<State> states = _stateRepository.Get(primaryOnly);
			foreach (State state in states)
				stateData.Add(state.Abbreviation);

			return stateData;
		}


		public ZipCodeData GetZipInfo(string zip)
		{

			ZipCodeData zipCodeData = null;
			ZipCode zipCodeEntity = _zipCodeRepository.GetByZip(zip);

			if (zipCodeEntity != null)
			{
				zipCodeData = new ZipCodeData()
				{
					City = zipCodeEntity.City,
					State = zipCodeEntity.State.Abbreviation,
					ZipCode = zipCodeEntity.Zip
				};
			}
			return zipCodeData;
		}


		public IEnumerable<ZipCodeData> GetZips(string state)
		{
			List<ZipCodeData> zipCodeData = new List<ZipCodeData>();

			var zips = _zipCodeRepository.GetByState(state);
			if (zips != null)
			{
				foreach(ZipCode zipCode in zips)
				{
					zipCodeData.Add(new ZipCodeData()
					{
						City = zipCode.City,
						State = zipCode.State.Abbreviation,
						ZipCode = zipCode.Zip
					});
				}
			}

			return zipCodeData;
		}

		public IEnumerable<ZipCodeData> GetZips(string zip, int range)
		{
			List<ZipCodeData> zipCodeData = new List<ZipCodeData>();

			ZipCode zipEntity = _zipCodeRepository.GetByZip(zip);
			var zips = _zipCodeRepository.GetZipsForRange(zipEntity, range);
			if (zips != null)
			{
				foreach (ZipCode zipCode in zips)
				{
					zipCodeData.Add(new ZipCodeData()
					{
						City = zipCode.City,
						State = zipCode.State.Abbreviation,
						ZipCode = zipCode.Zip
					});
				}
			}

			return zipCodeData;
		}
	}
}
