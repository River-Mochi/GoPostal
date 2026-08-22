// <copyright file="LocalePT_BR.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// LocalePT_BR.cs
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
                { m_Setting.GetSettingsLocaleID(), "Magic Mail + Postal Dispatch" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kActionsTab), "Ações" },
                { m_Setting.GetOptionTabLocaleID(Setting.kStatusTab), "Status" },
                { m_Setting.GetOptionTabLocaleID(Setting.kAboutTab), "Sobre" },

                // Groups (Actions tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "Ajuda na entrega postal" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostVanGroup), "Vans e caminhões postais" },
                { m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Centro de triagem" },
                { m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "Redefinir" },

                // Groups (Status tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusSummaryGroup), "Varredura da cidade" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusActivityGroup), "Última atualização" },

                // Groups (About tab)
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutInfoGroup), "Informações" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAboutLinksGroup), "Links" },

                // ---- Post Office ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Corrigir pouco correio local" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)),
                    "Quando ativado, aparece um pouco de correio se o estoque ficar baixo demais.\n" +
                    "Não cria vans extras; é tipo mágica... mas de verdade :)"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Limite de correio local" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)),
                    "Se o correio local cair abaixo da porcentagem que você escolher,\n" +
                    "a agência busca mais correio local.\n" +
                    "É uma porcentagem da capacidade máxima do prédio.\n" +
                    "Ex.: <armazenamento máx. = 100.000> e <limite = 5%>,\n" +
                    "quando o correio local < <5.000>, mais correio é buscado."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Quantidade de correio local" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)),
                    "Porcentagem adicionada ao buscar correio local (reposição mágica).\n" +
                    "Se o máximo vanilla = <100.000> e estiver em <10%>,\n" +
                    "<10.000> são adicionados quando necessário."
                },

                // Global overflow toggle (PO + PSF)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FixMailOverflow)), "Corrigir excesso de correio" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.FixMailOverflow)),
                    "Quando há correio demais, as instalações fazem uma pequena limpeza mágica.\n" +
                    "O correio armazenado em excesso é tratado como entregue e removido.\n" +
                    "Isso evita que as instalações fiquem travadas cheias para sempre.\n" +
                    "Desative para manter o comportamento vanilla puro."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Limite de excesso da agência" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)),
                    "Quando o total de correio numa agência chega a esta porcentagem, o mod\n" +
                    "remove o suficiente para voltar a este nível."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Limite de excesso do centro de triagem" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)),
                    "Quando o total de correio num centro de triagem chega a esta porcentagem, o mod\n" +
                    "remove o suficiente para voltar a este nível."
                },

                // ---- Post Vans & Trucks ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ChangeCapacity)), "Alterar capacidades" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ChangeCapacity)),
                    "Ative para mudar as capacidades de vans e caminhões. Quando desligado,\n" +
                    "os controles de capacidade abaixo ficam ocultos e\n" +
                    "os valores vanilla do jogo são usados mesmo que os controles tenham outros valores."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanMailLoadPercentage)), "Carga da van postal" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanMailLoadPercentage)),
                    "Controla quanto correio cada van postal pode carregar.\n" +
                    "<100% = carga vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PostVanFleetSizePercentage)), "Tamanho da frota de vans" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PostVanFleetSizePercentage)),
                    "Controla quantas vans cada prédio postal pode ter e despachar.\n" +
                    "<100% = frota vanilla.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TruckCapacityPercentage)), "Tamanho da frota de caminhões" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.TruckCapacityPercentage)),
                    "Controla quantos caminhões postais cada centro de triagem (e qualquer instalação com caminhões postais)\n" +
                    "pode ter e despachar.\n" +
                    "<100% = frota vanilla.>"
                },

                // ---- Sorting Facility ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)), "Velocidade de triagem" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_SortingSpeedPercentage)),
                    "Multiplicador dos centros de **triagem**. Aplica-se à velocidade base de triagem.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)), "Capacidade de armazenamento" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_StorageCapacityPercentage)),
                    "Controla o **armazenamento de correio**.\n" +
                    "<100% = vanilla>."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Corrigir pouco correio não triado" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)),
                    "Quando ativado, aparece um pouco de correio não triado se o estoque ficar baixo demais.\n" +
                    "Isso mantém os centros de triagem funcionando.\n" +
                    "É uma solução temporária para um bug atual em que os centros de triagem não recebem correio suficiente quando há um porto de carga."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Limite de correio não triado" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)),
                    "Se o correio não triado cair abaixo desta pequena porcentagem da capacidade total,\n" +
                    "um pouco mais de correio não triado é buscado."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Quantidade de correio não triado" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)),
                    "Correio extra adicionado ao buscar correio não triado (reposição mágica).\n" +
                    "A quantidade é uma porcentagem da capacidade máxima.\n" +
                    "Se vanilla <máx. = 250.000> e estiver em <10%>, então <25.000> são adicionados."
                },

                // ---- RESET BUTTONS ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Padrões do jogo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "Restaura todas as configurações para o comportamento padrão original do jogo (vanilla)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Recomendado" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)),
                    "**Início rápido** – aplica todas as configurações postais recomendadas.\n" +
                    "Modo fácil: 1 clique e pronto!"
                },

                // ---- Status tab ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusFacilitySummary)), string.Empty },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusFacilitySummary)), "Resumo das agências, vans, centros de triagem e caminhões processados na última varredura em segundo plano." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusCityMailSummary)), "Correio mensal" },
                {
                    m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusCityMailSummary)),
                    "Mostra o fluxo recente de correio na cidade inteira.\n" +
                    "\n" +
                    "**Acumulado** = quanto correio os cidadãos geraram.\n" +
                    "**Processado** = quanto correio a rede realmente conseguiu processar.\n" +
                    "\n" +
                    "- Se Processado costuma ser maior que Acumulado, sua rede postal tem capacidade suficiente.\n" +
                    "- Se Acumulado ficar acima de Processado por muito tempo,\n" +
                    "a cidade está gerando mais correio do que a rede consegue lidar.\n" +
                    "Adicione instalações ou vans, ou ajuste suas configurações."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StatusLastActivity)), "Atividade" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StatusLastActivity)), "Conta as reposições de correio e limpezas de excesso feitas na última atualização." },

                // ---- Status text templates (for MagicMailSystem) ----
                { "MM_STATUS_NO_FACILITIES", "Nenhuma instalação postal foi processada ainda. Abra uma cidade e deixe a simulação rodar." },

                { "MM_STATUS_NO_ACTIVITY", "Nenhuma atividade registrada ainda." },

                { "MM_STATUS_SUMMARY", "{0} agências | {1} vans postais | {2} centros de triagem | {3} caminhões postais" },

                { "MM_STATUS_ACTIVITY", "{0} reposições locais | {1} reposições não triadas | {2} limpezas de excesso" },

                { "MM_STATUS_CITY_MAIL_NOT_READY", "As estatísticas de correio da cidade ainda não estão disponíveis. Abra uma cidade e deixe a simulação rodar." },

                { "MM_STATUS_CITY_MAIL", "{0} acumulado | {1} processado" },

                // ---- About tab: info ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Nome exibido deste mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Versão" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Versão atual do mod." },

                // ---- About tab: links ----
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadox)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadox)), "Abre a página da **Paradox** para **Magic Mail** e outros mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Abre o chat de feedback do **Discord** no navegador." },

            };
        }

        /// <summary>
        /// Called when the localization source is unloaded.</summary>
        public void Unload()
        {
        }
    }
}
