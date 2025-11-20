# PostMaster [PM]

PostMaster helps the city’s postal system work more smoothly and gives you more
control over how post offices and sorting facilities work.

It can:
- Top up mail at struggling post offices and sorting facilities.
- Increase post van payload and fleet sizes.
- Increase sorting speed and storage for sorting facilities.
- Trim overflowing mail buffers so facilities don’t stall.
- Surface simple city-wide mail stats in the options UI.

You can keep it close to vanilla or push the system much harder – all sliders
are optional and can be reset to game defaults at any time.

---

## What it does

### 1. Post offices – keep “mail to deliver” from running dry

**Goal:** Avoid the *“Unreliable mail service”* happiness malus by keeping
post offices supplied with local mail to deliver.

Options (Actions → *Post office* section):

- **Get local mail when it's too low**  
  When enabled, each post office checks its own local mail storage.  
  If it’s below a configurable threshold, the mod **adds a bit of local mail**
  directly into that building’s storage (no van trip required).

- **Local mail threshold (%)**  
  If local mail is below this % of capacity, the top-up triggers.

- **Local mail fetch amount (%)**  
  How much mail to add (as a % of capacity) when the top-up triggers.

This behaviour is the modernized version of the original Postal Helper:
instead of hard-coded “4000 units”, you control both the threshold and the
amount.

> ⚠️ This is a “magical” helper: it spawns extra local mail in the post office
> without simulating a van trip. Use it as a band-aid for transfer problems.

---

### 2. Sorting facilities – never starve the sorter

**Goal:** Keep sorting facilities busy so they can feed post offices with
sorted mail.

Options (Actions → *Sorting facility* section):

- **Get unsorted mail when it's too low**  
  When enabled, sorting facilities top up their own **unsorted mail** if it
  drops below a threshold.

- **Unsorted mail threshold (%)**  
  Below this %, the facility pulls in more unsorted mail.

- **Unsorted mail fetch amount (%)**  
  How much unsorted mail to add (as a % of capacity) when a top-up happens.

Like the post office option, this is also a “magical” helper:
it simulates extra incoming unsorted mail to keep the sorter from going idle.

---

### 3. Fix mail overflow (post offices + sorting)

**Goal:** Prevent facilities from becoming so overfilled with mail that they
effectively stall.

Options (Actions → *Post office* section):

- **Fix mail overflow (PO + sorting)**  
  One global toggle. When on, both post offices and sorting facilities will
  **trim back** their stored mail if they exceed configurable overflow
  thresholds.

- **Post office overflow threshold (%)**  
  If the *total* of local + outgoing + unsorted mail at a post office exceeds
  this %, the mod clamps it back down.

- **Sorting overflow threshold (%)**  
  Same idea, but for sorting facilities.

How it trims:

- The mod looks at the total mail vs capacity.
- If over the threshold, it computes a **target total** (e.g. 80% of capacity)
  and **reduces all three mail types proportionally** so that:
  - Ratios between local / outgoing / unsorted stay about the same,
  - Total storage drops back to something safe.

This replaces the original Postal Helper’s hard-coded overflow behaviour and
gives you control over both the on/off switch and the thresholds.

> 🔧 This also counts as “magical”: the mod **deletes some mail** when buffers
> are overfull to keep facilities from locking up.

---

### 4. Vans, trucks, and sorting power

**Goal:** Let you scale how powerful the postal network is without editing
game files.

Options (Actions → *Post vans & trucks* section):

- **Change capacities**  
  Master toggle. When off, all sliders below are ignored and the game behaves
  like vanilla.

- **Post van mail load (%)**  
  Multiplier for **how much mail a single post van can carry**  
  (`PostVanData.m_MailCapacity`).  
  - 100% = vanilla payload (e.g. 2t)  
  - Higher values let each van haul more mail (e.g. 300% → 6t).

- **Post van fleet size (%)**  
  Multiplier for **how many post vans each building can own**  
  (`PostFacilityData.m_PostVanCapacity`).  
  - Applies to post offices and any other facility with vans.

- **Post truck fleet size (%)**  
  Multiplier for **how many post trucks sorting facilities can own**  
  (`PostFacilityData.m_PostTruckCapacity`).  
  - Mainly affects sorting facilities and any prefab with post trucks.

Options (Actions → *Sorting facility* section):

- **Sorting speed (%)**  
  Multiplier for **sorting throughput** at sorting facilities  
  (`PostFacilityData.m_SortingRate`).

- **Sorting storage capacity (%)**  
  Multiplier for **storage** at sorting facilities  
  (`PostFacilityData.m_MailCapacity`, but only where the facility
  actually sorts mail).

These sliders are non-magical in the sense that they just make facilities
bigger/faster. They don’t invent mail on their own.

---

### 5. Status tab: quick city-level overview

Options (Status tab):

- **City scan**  
  Shows a one-line summary like:

  > `6 post offices | 55 post-vans | 1 sort building | 5 post trucks`

  This is based on the *current* capacities after your sliders are applied.

- **City mail**  
  Uses the vanilla `MailAccumulationSystem` to show:

  > `Monthly 168,192 accumulated | 277,759 processed`

  - **Accumulated** = how much new mail the city generated in the last
    measurement window.
  - **Processed** = how much mail was handled / delivered / resolved.

  If *processed* stays higher than *accumulated* over time, your postal system
  has enough capacity. If *accumulated* is often higher, the city is
  generating more mail than the system can handle.

- **Activity**  
  Shows how many local-mail top-ups, unsorted-mail top-ups and overflow
  cleanups were performed in the last update.

---

## “Magic” vs “non-magic” features

More “magic” / cheat-like:

- **Local mail top-up** (Get local mail when low)  
  Spawns extra local mail directly into a post office.

- **Unsorted mail top-up** (Get unsorted mail when low)  
  Spawns extra unsorted mail into sorting facilities.

- **Overflow cleanup**  
  Deletes excess stored mail above your overflow thresholds.

Less “magic”, more like realistic tuning:

- **Van payload slider** (more tons per van).
- **Van / truck fleet size sliders**.
- **Sorting speed and storage sliders**.
- **Status tab**, **city mail stats** (purely read-only).

---

## Debug “effective settings” string – do you need it?

You’re right:

- You can already **click a van** and see its payload (e.g. 6t instead of 2t).
- You can **click a post office** and see the new van / truck fleet size.

So a “debug string” that repeats:

> “Post vans: 6t payload, 15 vans per PO, 5 trucks per sorter…”

…is *purely optional*. It was just a quality-of-life idea for people who never
open building/vehicle info panels.

We can:

- Drop it completely, or
- Keep it only behind `#if DEBUG` if you ever want a quick sanity check.

---

## 8. What exactly still matches the original Postal Helper README?

From your old README:

> - Post Office gets Mail to Deliver when in critical need.  
> - Post Sorting Office gets Unsorted Mail when in critical need.

✅ Still basically correct, but now:

- “critical need” is configurable via thresholds and fetch amounts.
- We call the resources Local Mail / Unsorted Mail in the code.

> - If your postal infrastructure is working good and efficient then this mod will simply do nothing :)

Now it will **always**:

- Scale capacities if `Change capacities` is on.
- Trim overflow if `Fix mail overflow` is on.

So the “does nothing when things are fine” line is not quite true anymore —
but the “magic top-ups” are still only used when thresholds are crossed.

---

## 9. Idea for “2 / 20 vans out” (not enough vans dispatched)

You asked:

> what was the idea to help fix problem complaint where not enough delivery vans go out because buildings did not request?

The main vanilla logic is:

- **Buildings** (`MailAccumulationSystem.MailAccumulationJob.RequestPostVanIfNeeded`):
  - Compute `num` as:
    - `receivingMail` or `max(sendingMail, receivingMail)` depending on `m_RequireCollect`.
  - Only request a van if:

    ```csharp
    if (num < m_PostConfigurationData.m_MailAccumulationTolerance)
        return;
    ```

  - Then create a `PostVanRequest` with `Deliver` and sometimes `Collect`.

- **Mailboxes** (`MailBoxSystem.RequestPostVanIfNeeded`):
  - Only ever request **Collect** (since mailboxes don’t receive deliveries).

So when players see “2 / 20 vans out”:

- Usually it’s because **most buildings are under the tolerance** and never create requests,
- Not because GetVehicleCapacity is wrong *by itself*.

Possible non-Harmony fix ideas (not implemented yet):

1. **Lower global tolerance**  
   - From PostMaster, read the `PostConfigurationData` singleton and adjust
     `m_MailAccumulationTolerance` based on a slider.
   - Lower tolerance → more buildings qualify to request vans.

2. **Boost perceived mail amount**  
   - Before `MailAccumulationJob` runs, inflate `MailProducer`’s pending mail
     slightly so `num` crosses the tolerance more often.
   - This is more intrusive and risks weird side effects.

Right now, PostMaster **does not** touch these systems:

- We don’t modify `MailBoxSystem` or `MailAccumulationSystem`.
- We don’t create our own `PostVanRequest` entities.
- We just adjust capacities + do top-ups / overflow trims.

