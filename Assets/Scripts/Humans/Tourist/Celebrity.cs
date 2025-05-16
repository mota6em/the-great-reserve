public class Celebrity : Tourist
{
    public float reputationImpact = 0.2f;

    void Start()
    {
        name = "Celebrity";
        energy = 120; // maybe they last longer
    }

    public void GiveFeedback(bool satisfied)
    {
    //    float impact = satisfied ? reputationImpact : -reputationImpact;
      //  ParkReputation.Instance.Change(impact);
    }
}
