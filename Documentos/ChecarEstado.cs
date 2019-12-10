ng System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecarEstado : MonoBehaviour {

 public bool completou;
 float cronometro;

 MoveObject2D[] objetos;

 void Start () {
 cronometro = 0;
 completou = false;
 objetos = FindObjectsOfType<MoveObject2D> ();
 }

 void Update () {
 cronometro += Time.deltaTime;
 if (cronometro >= 0.2f) { //5Hz
 cronometro = 0;
 int soma = 0;
 for (int x = 0; x < objetos.Length; x++) {
 if (objetos [x].estaConectado) {
 soma++;
 }
 }
 if (soma >= objetos.Length) {
 completou = true;
 }
 }
 }
}
