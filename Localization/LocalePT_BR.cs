// Localization/LocalePT_BR.cs
// Brazilian Portuguese locale pt-BR

namespace MagicMail
{
    using System.Collections.Generic;
    using Colossal;

    /// <summary>
    /// Brazilian Portuguese localization source for Magic Mail [MM].</summary>
    public sealed class LocalePT_BR : IDictionarySource
    {
        private readonly Setting m_Setting;

        /// <summary>
        /// Constructs the Brazilian Portuguese locale generator.</summary>
        /// <param name="setting">Settings object used for locale IDs.</param>
        public LocalePT_BR(Setting setting)
        {
            m_Setting = setting;
        }

        /// <summary>
        /// Generates all Brazilian Portuguese localization entries for this mod.</summary>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod title
                { m_Setting.GetSettingsLocaleID(), "Magic Mail [MM]" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Ações" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab),  "Status" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab),   "Sobre" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup),          "Agência dos correios" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup),             "Vans e caminhões" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Centro de triagem" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup),               "Redefinir" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Visão geral da cidade" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Última atualização" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup),  "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Links" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Corrigir pouca correspondência local" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Quando ativado e o estoque fica muito baixo, aparece correspondência extra.\n " +
                    "Nenhuma van extra é gerada — é tipo mágica, mas funciona de verdade." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Limite de correio local" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Se a correspondência local cair abaixo desta porcentagem escolhida por você,\n " +
                    "a agência dos correios puxa mais correio local.\n" +
                    "É uma porcentagem da capacidade máxima de armazenamento." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Quantidade de correio local" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Porcentagem adicionada quando o correio local é recarregado (recarga mágica).\n" +
                    "Se o máximo vanilla = 10.000 e isto estiver em 20%, então 2.000 são adicionados."},

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "Corrigir excesso de correio" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Quando há correio demais, os edifícios fazem uma pequena limpeza **mágica**.\n " +
                    "O correio em excesso é tratado como entregue e removido.\n " +
                    "Isso evita que os edifícios fiquem presos para sempre como \"lotados\".\n " +
                    "Desative para manter o comportamento vanilla puro." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Limite de excesso (agência)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Quando o total de correio em uma agência dos correios chega a esta porcentagem,\n" +
                    "o mod apaga correio armazenado o suficiente para voltar a esse nível." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Limite de excesso (triagem)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                   "Quando o total de correio em um centro de triagem chega a esta porcentagem,\n" +
                   "o mod apaga correio armazenado o suficiente para voltar a esse nível." },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Alterar capacidades" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Ative isto para alterar as capacidades de vans e caminhões. Se estiver desativado,\n" +
                    "todos os controles deslizantes de capacidade abaixo ficam ocultos e\n" +
                    "os valores vanilla do jogo são usados mesmo que você tenha deixado outros valores nos sliders." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Carga das vans" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Controla quanta correspondência cada van pode transportar.\n" +
                    "<100% = carga vanilla.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Tamanho da frota de vans" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Controla quantas vans cada prédio postal pode possuir e despachar.\n" +
                    "<100% = frota vanilla.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Tamanho da frota de caminhões" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Controla quantos caminhões postais cada centro de triagem (e qualquer prédio com caminhões postais)\n " +
                    "pode possuir e despachar.\n " +
                    "<100% = frota vanilla.>" },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Velocidade de triagem" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Multiplicador para centros de **triagem**. Aplica-se à velocidade base de triagem do edifício.\n " +
                    "<100% = vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Capacidade de armazenamento da triagem" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Controla a **capacidade de armazenamento de correio**.\n " +
                    "<100% = vanilla>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Corrigir pouco correio não triado" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Quando ativado, um pouco de correio não triado aparece magicamente se o estoque ficar muito baixo.\n " +
                    "Isso mantém os centros de triagem ativos sem precisar esperar entregas.\n" +
                    "É uma correção temporária para um bug atual em que centros de triagem recebem pouco correio\n " +
                    "quando há um porto de carga." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Limite de correio não triado" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Se o correio não triado cair abaixo desta porcentagem da capacidade,\n " +
                    "um pouco de correio não triado extra é puxado magicamente.\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Quantidade de correio não triado" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Correio adicional adicionado quando o correio não triado é recarregado (recarga mágica).\n" +
                    "Valor em porcentagem da capacidade máxima de armazenamento." },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Padrões do jogo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)),
                    "Restaura todas as configurações para o comportamento padrão original do jogo (vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Recomendado" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**Início rápido** – aplica todas as configurações de correio recomendadas.\n" +
                    "Modo fácil: um clique e pronto!" },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), "" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)),
                    "Resumo das agências, vans, centros de triagem e caminhões processados\n " +
                    "na última atualização do jogo (~a cada 45 minutos no jogo)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Correio da cidade" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Mostra o fluxo recente de correio na cidade.\n\n" +
                     "**Acumulado** = quanto correio os cidadãos geraram.\n" +
                     "**Processado**   = quanto correio a rede realmente tratou.\n\n" +
                     "- Se \"Processado\" for frequentemente maior que \"Acumulado\", a rede postal tem capacidade suficiente.\n " +
                     "- Se \"Acumulado\" ficar acima de \"Processado\" por muito tempo, a cidade está gerando mais correio\n " +
                     "do que consegue tratar; adicione mais prédios, mais veículos ou ajuste as configurações." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Atividade" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)),
                    "Contagem de recargas de correio e limpezas de excesso feitas na última atualização." },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)),
                    "Nome exibido deste mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Versão" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)),
                    "Versão atual do mod." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)),
                    "Abre a página da **Paradox** para **Magic Mail** e outros mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)),
                    "Abre o canal de feedback no **Discord** no navegador." },
            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
