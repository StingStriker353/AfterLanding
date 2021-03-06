﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
	private Image bgImg;
	private Image joystickImg;
	private Vector3 inputVector;

	private void Start() {
		bgImg = GetComponent<Image>();
		joystickImg = transform.GetChild(0).GetComponent<Image>();
	}

	public virtual void OnDrag(PointerEventData eventData) {
		Vector2 pos;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos)) {
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			inputVector = new Vector3(( pos.x * 2), (pos.y * 2), 0);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3), inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3));
		}
	}

	public virtual void OnPointerDown(PointerEventData eventData) {
		OnDrag(eventData);
	}

	public virtual void OnPointerUp(PointerEventData eventData) {
		if (gameObject.name != "Rotation Joystick") {
			inputVector = Vector3.zero;
		}
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
	}

	public float horizontal() {
		if(inputVector.x != 0) {
			return inputVector.x;
		} else {
			return Input.GetAxis("Horizontal");
		}
	}

	public float vertical() {
		if (inputVector.y != 0) {
			return inputVector.y;
		} else {
			return Input.GetAxis("Vertical");
		}
	}

	public float rotHorizontal() {
		if (inputVector.x != 0) {
			return inputVector.x;
		} else {
			return Input.GetAxis("Horizontal");
		}
	}

	public float rotVertical() {
		if (inputVector.y != 0) {
			return inputVector.y;
		} else {
			return Input.GetAxis("Vertical");
		}
	}
}
