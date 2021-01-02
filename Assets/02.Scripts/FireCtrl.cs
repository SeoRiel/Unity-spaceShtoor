using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총알 발사 및 재장전 오디오 클립 저장에 필요한 구조체
[System.Serializable]
public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip[] reload;
}

public class FireCtrl : MonoBehaviour
{
    private AudioSource weaponAudio;                              // Audio Component 저장 변수

    public enum WeaponType                                          // 무기 종류
    {
        RIFLE = 0,
        SHOTGUN
    }

    public WeaponType currWeapon = WeaponType.RIFLE;     // 캐릭터가 현재 들고 있는 무기 저장 변수
    public GameObject bullet;                                           // 총알 프리팹 생성
    public Transform firePos;                                            // 총알 발사 방향
    // public ParticleSystem cartridge;                                // 탄피 추출 이펙트
    public ParticleSystem muzzleFlash;                               // 총구 화염 이펙트
    public PlayerSfx playerSfx;                                          // 오디오 클립 저장 변수

    // Start is called before the first frame update
    private void Start()
    {
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();   // firePos의 하위 컴포넌트 추출
        weaponAudio = GetComponent<AudioSource>();                         // Audio Component 추출
    }

    // Update is called once per frame
    private void Update()
    {
        // 마우스 왼쪽 클릭 시, Fire 함수 호출
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    // Bullet Prefab 복제
    private void Fire()
    {
        // Bullet Prefab 동적 생성
        Instantiate(bullet, firePos.position, firePos.rotation);

        // Instantiate의 다형성은 아래와 같음
        // Instantiate<GameObject>(bullet, firePos.position, firePos.rotation);
        // Instantiate<GameObject>(bullet, firePos.position, firePos.rotation, null);
        // Instantiate<GameObejct>(bullet, firePos);
        // Instantiate<GameObject>(bullet, firePos, false);


        // cartridge.Play();        // 파티클 실행
        muzzleFlash.Play();       // 총구 화염 파티클 실행
        FireSFX();                   // 사운드 출력
    }

    // 
    private void FireSFX()
    {
        var useAudio = playerSfx.fire[(int)currWeapon];       // 현재 사용 중인 무기 오디오 클립 호출

        weaponAudio.PlayOneShot(useAudio, 1.0f);           // 사운드 출력
    }
}
