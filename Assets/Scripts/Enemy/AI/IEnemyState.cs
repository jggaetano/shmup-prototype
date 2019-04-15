public interface IEnemyState {

    bool Passive { get; }
    bool OneTime { get; }

    void UpdateState(float deltaTime);

}
