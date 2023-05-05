using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageTurn : MonoBehaviour
{
  
    [SerializeField] GameObject forwardButton;
    [SerializeField] GameObject backButton;
    
    public int currentPage;
    public int totalPages; 
    List<Transform> list;
   
 void Start()
    {
        totalPages = gameObject.transform.childCount;
        Debug.Log(totalPages);

        list = GetChildren(transform);
        SetPage(0);

        // just for now (actually this works, i'll leave it)
        backButton.SetActive(false);

        /*foreach (Transform page in list)
        {
            Debug.Log(page.name);
        }*/

    }
    
    public List<Transform> GetChildren(Transform parent)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parent)
        {
            children.Add(child);
        }
        return children;
    }

    public void SetPage(int index)
    {
        // set current page to disable(currentPage)
        list[currentPage].gameObject.SetActive(false);
        // get specific page index and set to active from list
        list[index].gameObject.SetActive(true);
    }
    public void NextPage()
    {
        //go to next page
        if(currentPage < totalPages - 1)
        {
            SetPage(currentPage + 1);
            currentPage++;
        }   
        
        // makes buttons active or inactive depending on place in pages
        if(backButton.activeInHierarchy == false)
        {
            backButton.SetActive(true);
        }
        if(currentPage == totalPages - 1)
        {
            forwardButton.SetActive(false);
        }
    }

    public void PrevPage()
    {
         //go to previous page
        if(currentPage > 0 )
        {
            SetPage(currentPage - 1);
            currentPage--;
        }   
 
        // makes buttons active or inactive depending on place in pages
        if(forwardButton.activeInHierarchy == false)
        {
            forwardButton.SetActive(true);
        }
        if(currentPage == 0)
        {
            backButton.SetActive(false);
        }
    }
    
   
}
