using UnityEngine; 
using System.Collections.Generic; 
public static class SD_WordTranslate { 
	public static Dictionary<string, Class_WordTranslate> Class_Dic = JsonReader.ReadJson<Class_WordTranslate> ("Json/Language/WordTranslate");
	}
