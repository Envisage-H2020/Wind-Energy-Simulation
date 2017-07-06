using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace goedle_sdk.detail
{
	public class GoedleAnalytics
	{
		private string api_key = null;
		private string app_key = null;
		private string user_id = null;
		private string anonymous_id = null;
		private string app_version = null;

		public GoedleAnalytics (string api_key, string app_key, string user_id, string app_version)
		{
			this.api_key = api_key;
			this.app_key = app_key;
			this.user_id = user_id;
			this.app_version = app_version;

			track_launch ();
		}

		public void set_user_id(string user_id){
			this.anonymous_id = this.user_id;
			this.user_id = user_id;
			track (GoedleConstants.IDENTIFY, null, null, false, null, null, this.anonymous_id);
		}

		public void track_launch ()
		{
			track (GoedleConstants.EVENT_NAME_INIT, null, null, true, null, null, null);
		}


		public void track (string event_name, string event_id, string event_value, bool launch, string trait_key, string trait_value, string anonymous_id)
		{
			GoedleHttpClient outer = new GoedleHttpClient ();
			string[] pass = null;
			int ts = getTimeStamp ();
			// -1 because c# returns -1 for UTC +1 , * 1000 from Seconds to Milliseconds
			int timezone = (int)(((DateTime.UtcNow - DateTime.Now).TotalSeconds) * -1 * 1000);
			GoedleAtom rt = null;
			if (launch == true) {
				rt = new GoedleAtom (app_key, this.user_id, ts, event_name, event_id, event_value, timezone, GoedleConstants.BUILD_NR, app_version);
			} else if (event_name == "identify" && !string.IsNullOrEmpty (anonymous_id)) {
				rt = new GoedleAtom (app_key, this.user_id, ts, event_name, anonymous_id);
			} else if (event_name == "identify") {
				rt = new GoedleAtom (app_key, this.user_id, ts, event_name, event_id, event_value, trait_key, trait_value);
			} else {
				rt = new GoedleAtom (app_key, this.user_id, ts, event_name, event_id, event_value);
			}
			if (rt == null) {
				Console.Write ("Data Object is None, there must be an error in the SDK!");
			} else {
				pass = encodeToUrlParameter (rt.getGoedleAtomDictionary ());
			}
			outer.send (pass);
		}

		public void track (string event_name)
		{
			track (event_name, null, null, false, null, null, null);
		}
			

		public void track (string event_name, string event_id)
		{
			track (event_name, event_id, null, false, null, null, null);
		}

		public void track (string event_name, int event_id_i)
		{
			string event_id = event_id_i.ToString();

			track (event_name, event_id, null, false, null, null, null);
		}

		public void track (string event_name, string event_id, string event_value)
		{
			track (event_name, event_id, event_value, false, null, null, null);

		}


		public void track (string event_name, string event_id, string event_value, string trait_key, string trait_value)
		{
			track (GoedleConstants.IDENTIFY, null, null, false, trait_key, trait_value, null);
		}


		private string[] encodeToUrlParameter (Dictionary<string, object> goedleAtom)
		{
			string hashedAuthData = null;
			string content = "{";
			foreach (KeyValuePair<string, object> entry in goedleAtom) {
				if (entry.Key == "timezone" || entry.Key == "ts" || entry.Key == "build_nr") {
					content += "\"" + entry.Key + "\" : " + entry.Value + " , ";
				} else {
					content += "\"" + entry.Key + "\" : \"" + entry.Value + "\", ";
				}
			}
			int endIndex = content.LastIndexOf (",");
			content = content.Substring (0, endIndex);
			content += "}";
			Encoding enc = Encoding.UTF8;
			byte[] authData = enc.GetBytes (content + api_key);

			SHA1 sha = new SHA1CryptoServiceProvider ();
			hashedAuthData = HexStringFromBytes ((sha.ComputeHash (authData)));
			string[] pass = { content, hashedAuthData };
			return pass;
		}

		public string HexStringFromBytes (byte[] bytes)
		{
			var sb = new StringBuilder ();
			foreach (byte b in bytes) {
				var hex = b.ToString ("x2");
				sb.Append (hex);
			}
			return sb.ToString ();
		}

		public int getTimeStamp ()
		{
			return (Int32)(DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1))).TotalSeconds;
		}

	}
}