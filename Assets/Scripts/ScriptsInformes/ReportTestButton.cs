using UnityEngine;

public class ReportTestButton : MonoBehaviour
{
	public void GenerateTestReport()
	{
		ReportSaveSystem.CreateTestReport();

		Debug.Log("Informe de prueba generado.");
	}
}