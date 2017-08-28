using System;
using kCura.Relativity.Client.DTOs;
using UnlockApplication.Agent;

namespace UnlockApplication.Agent
{

	public partial class UnlockApplication : ArtifactWrapper
	{
		public string ApplicationGuid { get { return base.GetValue<string>(Guid.Parse(UnlockApplicationFieldGuids.ApplicationGuid)); } set { base.SetValue(Guid.Parse(UnlockApplicationFieldGuids.ApplicationGuid), value); } }
		public int? ArtifactID { get { return base.GetValue<int?>(Guid.Parse(UnlockApplicationFieldGuids.ArtifactID)); } set { base.SetValue(Guid.Parse(UnlockApplicationFieldGuids.ArtifactID), value); } }
		public string Name { get { return base.GetValue<string>(Guid.Parse(UnlockApplicationFieldGuids.Name)); } set { base.SetValue(Guid.Parse(UnlockApplicationFieldGuids.Name), value); } }
	}



}
