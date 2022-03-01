
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    private int slotNo = 0;
    public GameObject gun1, gun2, gun3, gun4;
    public int Level;
    public string WeaponChangeSound;

    void weaponpick(int slotNo)
    {
        if (slotNo == 0)
        {
            gun1.SetActive(true);
            gun2.SetActive(false);
            gun3.SetActive(false);
            gun4.SetActive(false);

        }
        else if (slotNo == 1)
        {
            gun1.SetActive(false);
            gun2.SetActive(true);
            gun3.SetActive(false);
            gun4.SetActive(false);
        }
        else if (slotNo == 2)
        {
            gun1.SetActive(false);
            gun2.SetActive(false);
            gun3.SetActive(true);
            gun4.SetActive(false);
        }
        else if (slotNo == 3) {
            gun1.SetActive(false);
            gun2.SetActive(false);
            gun3.SetActive(false);
            gun4.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            int currentSlot = slotNo;
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                slotNo += 1;
                if (slotNo > Level-1)
                {
                    slotNo = 0;
                }

            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                slotNo -= 1;
                if (slotNo < 0)
                {
                    slotNo = Level-1;
                }
            }
            if (currentSlot != slotNo) {
                FindObjectOfType<AudioManager>().SetnPlay(WeaponChangeSound, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
            }

            weaponpick(slotNo);
        }
    }
}
