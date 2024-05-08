using System.ComponentModel;

namespace SchoolFinder.Common.School.Model.Feedback
{
    public enum RatingCategory
    {
        [Description("Стан ремонту")]
        BuildingCondition = 1,
        [Description("Складність навчання")]
        LearningComplexity = 2,
        [Description("Вимогливіть педагогів")]
        TeachersDemanding = 3,
        [Description("Місце для паркування")]
        ParkingLots = 4,
        [Description("Успіхи учнів")]
        StudentSuccess = 5,
        [Description("Індивідуальний підхід")]
        IndividualApproach = 6,
        [Description("Інклюзивність будівлі")]
        InclusivenessOfBuilding = 7,
        [Description("Зручність розташування")]
        LocationConvenience = 8,
        [Description("Стан інфраструктури навколо")]
        InfrastructureCondition = 9,
    }
}
