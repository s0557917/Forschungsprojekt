using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using VrPassing.Networking;
using System.Collections.Specialized;
using System;

public class CubeManager : MonoBehaviourPun
{
    #region TESTING MATERIALS
    public Material mat1;
    public Material mat2;
    #endregion

    public Transform tableOne;
    public Transform tableTwo;

    private Vector3[] _cubePositions;

    private GameObject[] cubes;

    private bool _firstRun = true;
    public List<int> _visualizationMethodIndices = new List<int>() { 1, 2, 3, 4 };
    public int _currentVisualizationIndex;
    private System.Random random;

    private static CubeManager _instance;
    public static CubeManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {

        SpawnCubes();

        random = new System.Random();

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        _instance = this;
    }

    private void SpawnCubes()
    {
        FillPositionArray();

        cubes = new GameObject[_cubePositions.Length];
        for (int i = 0; i < _cubePositions.Length; i++)
        {
            GameObject cube = PhotonNetwork.Instantiate("PassingCube", _cubePositions[i], Quaternion.identity);

            if (PhotonNetwork.IsMasterClient)
            {
                cube.name = "PC - P1";
                cube.GetComponent<Renderer>().sharedMaterial = mat1;
            }
            else
            {
                cube.name = "PC - P2";
                cube.GetComponent<Renderer>().sharedMaterial = mat2;
            }

            cubes[i] = cube;
        }
    }

    private void FillPositionArray()
    {
        _cubePositions = new Vector3[10];
        Transform playerTable = PhotonNetwork.IsMasterClient ? tableOne : tableTwo;

        float yPosition = playerTable.position.y + 0.45f;
        float xFirstRow = playerTable.position.x;
        float xSecondRow = playerTable.position.x + 0.04f;
        float zStartingPosition = playerTable.position.z - 0.28f;

        _cubePositions[0] = new Vector3(xFirstRow, yPosition, zStartingPosition);
        _cubePositions[1] = new Vector3(xFirstRow, yPosition, zStartingPosition + (0.14f * 1));
        _cubePositions[2] = new Vector3(xFirstRow, yPosition, zStartingPosition + (0.14f * 2));
        _cubePositions[3] = new Vector3(xFirstRow, yPosition, zStartingPosition + (0.14f * 3));
        _cubePositions[4] = new Vector3(xFirstRow, yPosition, zStartingPosition + (0.14f * 4));
        _cubePositions[5] = new Vector3(xSecondRow, yPosition, zStartingPosition);
        _cubePositions[6] = new Vector3(xSecondRow, yPosition, zStartingPosition + (0.14f * 1));
        _cubePositions[7] = new Vector3(xSecondRow, yPosition, zStartingPosition + (0.14f * 2));
        _cubePositions[8] = new Vector3(xSecondRow, yPosition, zStartingPosition + (0.14f * 3));
        _cubePositions[9] = new Vector3(xSecondRow, yPosition, zStartingPosition + (0.14f * 4));
    }

    public void ResetCubePositions()
    {
        for (int i = 0; i < _cubePositions.Length; i++)
        {
            cubes[i].transform.position = _cubePositions[i];
        }
    }

    public void ResetCubePosition(GameObject cubeToReset)
    {
        //cubeToReset.transform.position = Array.Find(_cubePositions, item => item == cubeToReset);
    }

    public void GetNextVisualization()
    {
        if (_firstRun)
        {
            _firstRun = false;
            this.photonView.RPC("ApplySelectedVisualizationMethod", RpcTarget.All, 0);
        }
        else
        {
            int _selectedVisualization = _visualizationMethodIndices[random.Next(_visualizationMethodIndices.Count)];
            if (_visualizationMethodIndices.Contains(_selectedVisualization))
            {
                _visualizationMethodIndices.Remove(_selectedVisualization);
            }
            this.photonView.RPC("ApplySelectedVisualizationMethod", RpcTarget.All, _selectedVisualization);
        }
    }

    [PunRPC]
    private void ApplySelectedVisualizationMethod(int selectedVisualizationMethod)
    {
        _currentVisualizationIndex = selectedVisualizationMethod;
        if (_visualizationMethodIndices.Contains(selectedVisualizationMethod))
        {
            _visualizationMethodIndices.Remove(selectedVisualizationMethod);
        }
    }
}
