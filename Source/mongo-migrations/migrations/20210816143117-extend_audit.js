module.exports = {
  async up(db, client) {
    await db.collection('Users').updateMany({}, {$set: {Edited: null}});
    await db.collection('Users').updateMany({}, {$set: {EditedBy: "81a058a5-99cc-4576-8963-86be537179e8"}});
    await db.collection('Roles').updateMany({}, {$set: {EditedBy: "81a058a5-99cc-4576-8963-86be537179e8"}});
    await db.collection('Roles').updateMany({}, {$set: {Edited: null}});
    await db.collection('Sellers').updateMany({}, {$set: {EditedBy: "81a058a5-99cc-4576-8963-86be537179e8"}});
    await db.collection('Sellers').updateMany({}, {$set: {Edited: null}});
    await db.collection('Records').updateMany({}, {$set: {EditedBy: "81a058a5-99cc-4576-8963-86be537179e8"}});
    await db.collection('Records').updateMany({}, {$set: {Edited: null}});
  },

  async down(db, client) {
    await db.collection('Users').updateMany({}, {$unset: {EditedBy: 1}}, false, true);
    await db.collection('Roles').updateMany({}, {$unset: {EditedBy: 1}}, false, true);
    await db.collection('Sellers').updateMany({}, {$unset: {EditedBy: 1}}, false, true);
    await db.collection('Records').updateMany({}, {$unset: {EditedBy: 1}}, false, true);
    await db.collection('Sellers').updateMany({}, {$unset: {Edited: null}}, false, true);
    await db.collection('Records').updateMany({}, {$unset: {Edited: null}}, false, true);
    await db.collection('Roles').updateMany({}, {$unset: {Edited: null}}, false, true);
    await db.collection('Users').updateMany({}, {$unset: {Edited: null}}, false, true);
  }
};
