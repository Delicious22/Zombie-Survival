# 코딩 컨벤션

## 주석
함수에 대한 설명 밑에 매개변수에 대해 설명한다
visual studio 기준 함수 윗줄에 /// (슬래쉬 3개)를 입력 후 엔터를 누르면 자동생성된다.)
```C#
<summary>
hp를 add만큼 증가시키고 결과값을 반환
</summary> 
<param name="hp">체력</param> 
<param name="add">증가량</param> /// <returns></returns> 
private int AddHP(int hp, int add)
{
    hp += add; return hp;
}
```



## 변수명
|종류|컨벤션|비고|
|--|--|--|
|public 변수 | UpperCamelCase||
|protected 변수 |  lowerCamelCase||
|private 변수 | lowerCamelCase||
|const 변수 | 전부 대문자, _로 단어 구분||

```C#
public int PlayerHp
private int playerHp
public interface IInteractable
public const string NAME_SET = “asdasd”
```


## 메서드, 인터페이스
|종류|컨벤션|비고|
|--|--|--|
|메서드 | UpperCamelCase||
|인터페이스 | UpperCamelCase|접두어 I|



## 괄호
|종류| 컨벤션|비고|
|--|--|--|
|메서드 중괄호|내려서 쓴다||
|조건문 중괄호|내려서 쓴다|**한 줄**로 끝날 수 있으면 괄호를 **생략**하고 한 줄로 쓴다<br>**삼항연산자**가 짧으면 삼항연산자로 쓴다|
|반복문 중괄호|내려서 쓴다|**한 줄**로 끝날 수 있으면 괄호를 **생략**하고 한 줄로 쓴다|

```C#
// 중괄호를 내려서 쓴다.
int IncreaeHp (int Hp, int add)
{
   return Hp + add;
}
```

```C#
// 중괄호를 내려서 쓴다.
if ( isCheck ) 
{
    int add = 10;
    speed -= add;
}

// 한 줄로 끝날 수 있으면 괄호를 생략하고 한 줄로 쓴다
if ( isCheck ) speed -= add;
```

```C#
// 중괄호를 내려서 쓴다.
for ( int i = 0; i < N; i++ ) 
{
    int add = 10;
    speed -= add;
}

// 한 줄로 끝날 수 있으면 괄호를 생략하고 한 줄로 쓴다
for ( int i = 0; i < N; i++ ) speed -= add;
```

