namespace developwithpassion.bdd.mbunit.standard.observations
{
    public abstract class observations_for_a_sut_with_a_contract<Contract, ClassUnderTest> : observations_for_an_instance_sut<Contract, ClassUnderTest>
        where ClassUnderTest : Contract
    {

    }
}