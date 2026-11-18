
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
[XmlRoot("Leaderboard")]
public class LeaderboardData // este script la lista de resultados de un Kahoot.
{
    [XmlElement("Result")]
    public List<GameResult> results = new List<GameResult>();
}
