using System;

public class MuzzleFlashState : IEnemyState {

    private readonly EnemyController enemyController;
    private readonly Enemy enemy;

    private float timer;
    private float coolDown;

    public bool Passive {
        get; private set;
    }

    public bool OneTime {
        get; private set;
    }

    public MuzzleFlashState(EnemyController enemyController, Enemy enemy, float flashCoolDown, bool passive = false, bool oneTime = false) {
        this.enemyController = enemyController;
        this.enemy = enemy;
        timer = 0;
        coolDown = flashCoolDown;
        Passive = passive;
        OneTime = oneTime;
    }

    public void UpdateState(float deltaTime) {

        timer += deltaTime;
        if (timer >= coolDown) {
            for (int i = 0; i < enemyController.transform.childCount; i++) {
                if (enemyController.transform.GetChild(i).name == "Blaster") {
                    if (enemyController.transform.GetChild(i).childCount != 0) {
                        enemyController.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
                    }
                }
            }
            timer = 0.0f;
            enemy.NextState();
            return;
        }

        for (int i = 0; i < enemyController.transform.childCount; i++) {
            if (enemyController.transform.GetChild(i).name == "Blaster") {
                if (enemyController.transform.GetChild(i).childCount != 0) {
                    enemyController.transform.GetChild(i).GetChild(1).gameObject.SetActive(!enemyController.transform.GetChild(i).GetChild(1).gameObject.activeSelf);
                }
            }
        }

    }

}
