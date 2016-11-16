using System;

namespace NetTask3
{
	internal interface IGod
	{
		Human CreateHuman();
		Human CreateHuman(Gender gender);
		Human CreatePair(Human human);
	}
}

