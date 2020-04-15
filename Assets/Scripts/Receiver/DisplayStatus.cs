using System.Collections.Generic;
using UnityEngine;

public class DisplayStatus : MonoBehaviour
{
    [SerializeField] private List<Transform> miniatureHolder = null;
    [SerializeField] private ShapeDirectory directory = null;
    
    private int currentSlot = 0;
    private Receiver receiver = null;
    
    private void Awake()
    {
        receiver = this.GetComponent<Receiver>();
        receiver.addShapeEvent.AddListener(OnShapeAdded);
        receiver.removeShapeEvent.AddListener(OnShapeRemoved);

        for (currentSlot = 0; currentSlot < receiver.requiredShapes.Count; currentSlot++)
        {
            GameObject miniature = Instantiate(
                directory.GetMiniaturePrefab(receiver.requiredShapes[currentSlot]),
                miniatureHolder[currentSlot].position,
                Quaternion.identity,
                miniatureHolder[currentSlot]);

            miniature.GetComponent<Renderer>().material = directory.GetMaterial(Shapes.Null);
        }
    }

    private void OnShapeAdded(Shapes addedShape)
    {
        --currentSlot;
        Transform miniature = miniatureHolder[currentSlot].GetChild(0);
        miniature.GetComponent<Renderer>().material = directory.GetMaterial(addedShape);
    }

    private void OnShapeRemoved(Shapes removedShape)
    {
        Transform miniature = miniatureHolder[currentSlot].GetChild(0);
        miniature.GetComponent<Renderer>().material = directory.GetMaterial(Shapes.Null);
        ++currentSlot;
    }
}
