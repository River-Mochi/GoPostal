# Magic PostMaster [MPM]

Magic PostMaster helps Cities: Skylines II’s postal system work more smoothly and gives more
control over how post offices and sorting facilities behave.

It can:

- Top up **Local Mail** at struggling post offices.
- Top up **Unsorted Mail** at sorting facilities.
- Trim overflowing mail buffers so facilities don’t stall.
- Increase post van payload and fleet sizes.
- Increase post truck fleet sizes.
- Boost sorting speed and storage for sorting facilities.
- Show simple city-wide mail stats in the options UI.

Everything is optional: sliders can be turned off, and settings can always be reset
back to game defaults or to a recommended preset.

---

## 1. Post offices – keep “mail to deliver” available

**Goal:** Reduce the *“Unreliable mail service”* happiness penalty by keeping post offices
supplied with Local Mail to deliver.

**Options (Actions → Post office):**

- **Fix low Local mail**  
  When enabled, each post office checks its own Local Mail storage.  
  If it falls below a threshold, the mod **adds extra Local Mail** directly into the
  building’s buffer (no van trip required), so it has something to deliver.

- **Local mail threshold (%)**  
  If Local Mail is below this percentage of the building’s capacity, the top-up triggers.

- **Local mail fetch amount (%)**  
  How much Local Mail to add, as a percentage of the building’s capacity, when a top-up
  happens.

This is a configurable version of the original “Postal Helper” behaviour: instead of a
fixed amount, both the threshold and amount are under user control.

> ⚠️ This is a “magic” helper: Local Mail is spawned directly in the post office
> without simulating a transfer.

---

## 2. Sorting facilities – keep the sorter busy

**Goal:** Prevent sorting facilities from sitting idle because they have no Unsorted Mail to process.

**Options (Actions → Sorting facility):**

- **Fix low unsorted mail**  
  When enabled, a sorting facility checks its Unsorted Mail storage.  
  If it falls below a threshold, the mod **adds extra Unsorted Mail** into the facility.

- **Unsorted mail threshold (%)**  
  If Unsorted Mail is below this percentage of capacity, the facility triggers a top-up.

- **Unsorted mail fetch amount (%)**  
  How much Unsorted Mail to add, as a percentage of capacity, when a top-up happens.

This is also “magic”: Unsorted Mail appears in the facility to keep the sorting line busy
without waiting for additional trucks or outside connections.

---

## 3. Fix mail overflow (post offices + sorting)

**Goal:** Stop postal buildings from becoming permanently overfilled with mail and stalling.

Both **post offices** and **sorting facilities** use three stored resources:

- Local Mail  
- Outgoing Mail  
- Unsorted Mail  

When enabled, the overflow fixer looks at the **total** of all three.

**Options (Actions → Post office):**

- **Fix mail overflow**  
  Global toggle. When on, both post offices and sorting facilities trim their stored mail
  if they exceed configurable overflow thresholds.

- **Post office overflow threshold (%)**  
  If total stored mail (Local + Outgoing + Unsorted) at a post office exceeds this
  percentage of its capacity, a cleanup is performed.

- **Sorting overflow threshold (%)**  
  Same logic applied to sorting facilities.

**How trimming works (both building types):**

- The current total mail is compared to capacity.  
- If total mail exceeds the building’s overflow threshold, a **target total** is computed  
  (for example, capacity 10 000 and threshold 85% ⇒ target total 8 500).
- All three mail types (Local, Outgoing, Unsorted) are **scaled down proportionally** so:
  - Their ratios stay roughly the same, and  
  - The new total is pulled down toward the target.

In practical terms, this treats some stored mail as “delivered/processed” so the building
can start working again instead of staying clogged forever.

> 🔧 This is also “magic”: some mail is deleted to prevent permanent congestion.

---

## 4. Vans, trucks, and sorting power

**Goal:** Adjust the strength of the postal network without editing game files.

### 4.1 Post vans & trucks

**Options (Actions → Post vans & trucks):**

- **Change capacities**  
  Master toggle. When off, the game uses pure vanilla values and hides the sliders below.
  When on, the sliders override the vanilla capacities.

- **Post van mail load (%)**  
  Multiplier for how much mail a single post van can carry  
  (`PostVanData.m_MailCapacity`).  
  - 100% = vanilla payload.  
  - Higher values allow each van to carry more mail.

- **Post van fleet size (%)**  
  Multiplier for how many post vans each postal building can own  
  (`PostFacilityData.m_PostVanCapacity`).  
  - Applies to post offices and other post facilities using vans.

- **Post truck fleet size (%)**  
  Multiplier for how many post trucks each sorting facility can own  
  (`PostFacilityData.m_PostTruckCapacity`).  
  - Mainly affects sorting facilities and other prefabs with post trucks.

### 4.2 Sorting facilities

**Options (Actions → Sorting facility):**

- **Sorting speed (%)**  
  Multiplier for the facility’s sorting throughput  
  (the game’s `m_SortingRate` value).  
  - 100% = vanilla sorting speed.  
  - Higher values let a sorting facility process more mail per tick.

- **Sorting storage capacity (%)**  
  Multiplier for how much mail a sorting facility can store  
  (`PostFacilityData.m_MailCapacity`, but only for facilities that actually sort).

These capacity changes are non-magical: they simply increase or decrease how strong the
network is. No mail is created or destroyed by these sliders alone.

---

## 5. Status tab – quick city overview

The **Status** tab is read-only and reflects the current state of the city.

**Groups:**

- **City scan**  
  Shows a one-line summary, for example:  
  `6 post offices | 55 post-vans | 1 sort building | 5 post trucks`  
  These counts reflect the effective capacities after sliders are applied.

- **City mail**  
  Uses the vanilla `MailAccumulationSystem` to summarize recent city-wide mail flow, for example:  
  `Monthly   168,192 accumulated | 277,759 processed`

  - **Accumulated** = how much mail citizens generated in the recent window.  
  - **Processed**   = how much mail the network actually handled.

  If **Processed** stays above **Accumulated** over time, the postal network has
  enough capacity and the postal budget could potentially be reduced.  
  If **Accumulated** consistently exceeds **Processed**, the city is generating more
  mail than the network can handle and more capacity or different settings are needed.

- **Activity**  
  Shows counts for:
  - Local-mail top-ups at post offices.  
  - Unsorted-mail top-ups at sorting facilities.  
  - Overflow cleanups at both building types.

This is useful for checking whether the “magic” helpers are doing anything or if the city
is already running fine without them.

---

## 6. “Magic” vs “non-magic” features

**Magic / cheat-like behaviour:**

- **Fix low Local mail** (post offices)  
  Spawns Local Mail directly into the building when storage is too low.

- **Fix low unsorted mail** (sorting facilities)  
  Spawns Unsorted Mail directly into the facility when storage is too low.

- **Fix mail overflow**  
  Deletes excess stored mail above chosen thresholds to keep buildings from blocking.

**More realistic tuning:**

- Post van mail load slider.  
- Post van fleet size slider.  
- Post truck fleet size slider.  
- Sorting speed slider.  
- Sorting storage capacity slider.  
- Status / City mail information (purely observational).

---

## 7. Safety

- Does **not** patch game DLLs directly.  
- Only changes prefab capacities and building resource buffers at runtime.  
- Does **not** permanently modify the save file structure.

It is safe to:

- Disable the mod in the launcher, or  
- Unsubscribe from the mod.

On the next load without the mod:

- Facilities return to the game’s default capacities.  
- Any “magic” mail that was added or removed only existed in the simulation at the
  time and does not corrupt the save.
