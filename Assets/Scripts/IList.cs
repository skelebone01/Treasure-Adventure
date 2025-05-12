using UnityEngine;
//an interface class for linked list and double linked list
public interface IList
{
    Node head { get; }
    void BuildList(Transform[] transforms, GameObject[] visuals);
    Node FindByIndex(int index);

}
