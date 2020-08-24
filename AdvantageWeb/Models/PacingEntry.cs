using System;
using System.Collections.Generic;

namespace AdvantageWeb.Models
{
    public partial class PacingEntry
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public string ClientDivisionProduct { get; set; }
        public string Platform { get; set; }
        public string CampaignDescription1 { get; set; }
        public string EntryName { get; set; }
        public string Client { get; set; }
        public string CampaignName { get; set; }
        public string PackageName { get; set; }
        public string CampaignDescription2 { get; set; }
        public string Vendor { get; set; }
        public string Tactic { get; set; }
        public string AdFormat { get; set; }
        public string Strategist1 { get; set; }
        public string Specialist { get; set; }
        public string Goal { get; set; }
        public string PrimaryKpi { get; set; }
        public string Channel { get; set; }
        public string SecondaryKpi { get; set; }
        public string VerticalSubvertical { get; set; }
        public string Identifier { get; set; }
        public string Strategist2 { get; set; }
        public string CampaignIo { get; set; }
        public string PrimaryKpiValue { get; set; }
        public string SecondaryKpiValue { get; set; }
        public decimal? IoCpm { get; set; }
        public int? PlannedImpressions { get; set; }
        public decimal? PlatformCommission { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string PlatformLink { get; set; }
        public string DailyBudgetEven { get; set; }
        public DateTime? Flight10End { get; set; }
        public decimal? Flight10NetBudget { get; set; }
        public int? Flight10PlannedImpressions { get; set; }
        public DateTime? Flight10Start { get; set; }
        public DateTime? Flight11End { get; set; }
        public decimal? Flight11NetBudget { get; set; }
        public int? Flight11PlannedImpressions { get; set; }
        public DateTime? Flight11Start { get; set; }
        public DateTime? Flight12End { get; set; }
        public decimal? Flight12NetBudget { get; set; }
        public int? Flight12PlannedImpressions { get; set; }
        public DateTime? Flight12Start { get; set; }
        public DateTime? Flight13End { get; set; }
        public decimal? Flight13NetBudget { get; set; }
        public int? Flight13PlannedImpressions { get; set; }
        public DateTime? Flight13Start { get; set; }
        public DateTime? Flight14End { get; set; }
        public decimal? Flight14NetBudget { get; set; }
        public int? Flight14PlannedImpressions { get; set; }
        public DateTime? Flight14Start { get; set; }
        public DateTime? Flight15End { get; set; }
        public decimal? Flight15NetBudget { get; set; }
        public int? Flight15PlannedImpressions { get; set; }
        public DateTime? Flight15Start { get; set; }
        public DateTime? Flight1End { get; set; }
        public decimal? Flight1NetBudget { get; set; }
        public int? Flight1PlannedImpressions { get; set; }
        public DateTime? Flight1Start { get; set; }
        public DateTime? Flight2End { get; set; }
        public decimal? Flight2NetBudget { get; set; }
        public int? Flight2PlannedImpressions { get; set; }
        public DateTime? Flight2Start { get; set; }
        public DateTime? Flight3End { get; set; }
        public decimal? Flight3NetBudget { get; set; }
        public int? Flight3PlannedImpressions { get; set; }
        public DateTime? Flight3Start { get; set; }
        public DateTime? Flight4End { get; set; }
        public decimal? Flight4NetBudget { get; set; }
        public int? Flight4PlannedImpressions { get; set; }
        public DateTime? Flight4Start { get; set; }
        public DateTime? Flight5End { get; set; }
        public decimal? Flight5NetBudget { get; set; }
        public int? Flight5PlannedImpressions { get; set; }
        public DateTime? Flight5Start { get; set; }
        public DateTime? Flight6End { get; set; }
        public decimal? Flight6NetBudget { get; set; }
        public int? Flight6PlannedImpressions { get; set; }
        public DateTime? Flight6Start { get; set; }
        public DateTime? Flight7End { get; set; }
        public decimal? Flight7NetBudget { get; set; }
        public int? Flight7PlannedImpressions { get; set; }
        public DateTime? Flight7Start { get; set; }
        public DateTime? Flight8End { get; set; }
        public decimal? Flight8NetBudget { get; set; }
        public int? Flight8PlannedImpressions { get; set; }
        public DateTime? Flight8Start { get; set; }
        public DateTime? Flight9End { get; set; }
        public decimal? Flight9NetBudget { get; set; }
        public int? Flight9PlannedImpressions { get; set; }
        public DateTime? Flight9Start { get; set; }
        public string MonthlyBudgetRollOver { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; }
        public string PackageDescription { get; set; }
        public decimal NetBudget { get; set; }
        public string ProjectManager { get; set; }

        public virtual PacingClient ClientNavigation { get; set; }
        public virtual AuthUser User { get; set; }
    }
}
