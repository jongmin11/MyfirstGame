# Metaverse

사람들돠 같이 미니게임을 즐길수있는 메타버스 프로젝트(멀티없음)

그리고 혹시몰라서 무료에셋 다운받은건 안올리겠습니다

#### 이동방식 :WASD

#### 달리기/걷기 : 왼쪽쉬프트/왼쪽컨트롤

#### 대화: E

##### **주의할점! 상하좌우가아님 대각선으로만 움직임**

탑다운 미니게임은 못만들었음



#### 메인씬

**AniManagers.cs**

플레이어(또는 캐릭터)의 방향, 이동, 걷기/달리기 애니메이션 및 승리/패배 포즈 애니메이션을 제어함

**DialogueData.cs**

대화 데이터를 저장하는 ScriptableObject임

여러 줄의 대사를 배열로 보유하며, 각 줄은 DialogueLine으로 구성됨

**DialogueLine.cs**

speakerName: 말하는 사람의 이름

text: 대사 내용

expression: 표정 이미지

triggerSceneChange: 씬 전환 여부

nextSceneName: 전환할 씬 이름

**DialogueManager.cs**

대화 시스템의 핵심 매니저

대화 시작, 한 줄씩 넘기기, 대화 종료, 자동 씬 전환 기능을 담당

E키로 다음 대사 진행, Q키로 대화 종료.

**NPCInteraction.cs**

NPC와의 상호작용을 담당

플레이어가 가까이 있으면 E키로 대화를 시작할 수 있으며, 대화가 끝나면 초기화

OnTriggerEnter2D, OnTriggerExit2D로 플레이어 위치 감지.

**MainSceneBestScoreUI.cs**

메인 씬에서 각 미니게임(씬)의 최고 점수를 PlayerPrefs에서 불러와 텍스트로 출력

sceneName\_BestScore 형식으로 저장된 키를 사용해 값을 표시

**SceneFader.cs**

씬 전환 시 페이드 인/아웃 효과와 로딩 슬라이더 UI를 담당하는 싱글톤 매니저.

FadeAndLoadScene(sceneName)으로 부드러운 씬 전환을 구현하며, Resources/LoadingCanvas 프리팹이 필요



#### **블럭쌓기미니게임(오른쪽)**

**BaseUI.cs**

모든 UI 클래스들의 기본 추상 클래스.

UI 상태(UIState)에 따라 해당 UI 오브젝트의 활성화 여부를 제어하는 공통 메서드 포함.

**DestroyZone.cs**

충돌한 오브젝트가 "Rubble" 이름이면 삭제하는 영역.

떨어진 블록 조각 처리용.

**GameUI.cs**

게임 도중 점수, 콤보, 최대 콤보를 표시하는 UI.

BaseUI를 상속하며 UIState.Game 상태일 때 표시됨.

**HomeUI.cs**

게임 시작 화면의 UI.

시작 및 종료 버튼을 갖고 있으며, UIState.Home 상태에서 활성화됨.

버튼 클릭 시 UIManager를 통해 게임 시작 혹은 종료 처리.

**ScoreUI.cs**

게임 종료 후 점수, 콤보, 최고 점수, 최고 콤보를 표시하는 UI.

UIState.Score 상태에서 표시되며, 재시작 또는 종료 버튼도 포함.

**TheStack.cs**

스택 블록 쌓기 미니게임의 메인 로직.

블록 생성 및 이동

정확도 판정 및 콤보 처리

점수 저장 (PlayerPrefs)

게임 오버 및 리스타트 처리

색상 전환 및 파괴 블록 생성

**UIManager.cs**

UI 상태를 전환하고 각 UI를 초기화/갱신하는 매니저.

TheStack과 연결되어 점수 갱신 및 상태 변경을 담당.

싱글톤 구조이며 Home, Game, Score UI 모두 관리.



#### 비행기미니게임(위쪽)

**BgLooper.cs**

배경과 장애물을 순환(loop)시키는 컴포넌트.

배경 오브젝트가 화면을 벗어나면 다시 앞으로 이동시키고, 장애물은 무작위 위치에 재배치.

"Coin" 태그의 오브젝트는 충돌 시 삭제됨.

**Camera.cs**

플레이어를 따라가는 카메라 스크립트.

target을 따라 X축으로만 이동하며, 처음 위치 기준 오프셋 유지.

**CoinSpawner.cs**

일정 시간마다 코인을 랜덤한 위치에 생성.

spawnInterval 간격마다 생성 시도하며, 충돌 검사 후 겹치지 않으면 생성.

생성 위치는 offsetMin ~ offsetMax 범위 내에서 결정됨.

**GameManager.cs**

게임 시작 UI, 점수 UI, 게임 오버 UI를 제어.

점수 증가, 게임 오버 처리, 최고 점수 저장, 씬 리스타트 및 메인으로 이동 처리.

씬 최초 진입 시 페이드 인 처리 포함

**ItemScoreGiver.cs**

플레이어와 충돌 시 점수를 추가하는 아이템(예: 코인).

점수는 scoreValue 값만큼 추가되며, coinSprite로 비주얼 설정 가능.

에디터에서 Gizmo 및 이름 자동 설정 기능 포함.

**Opstacle.cs**

장애물을 무작위 위치에 배치하기 위한 스크립트.

위쪽/아래쪽 오브젝트의 Y 위치는 고정된 리스트에서 선택.

구멍 크기와 간격도 조절 가능하며, SetRandomPlace()로 재배치 수행.

**Player.cs**

자동으로 앞으로 이동하며, 스페이스/마우스 클릭으로 점프 가능.

중력 적용 및 점프에 따라 캐릭터 회전 각도 조절.

장애물 충돌 시 GameManager 통해 게임 오버 처리.

godMode가 켜져 있으면 충돌 무시.

