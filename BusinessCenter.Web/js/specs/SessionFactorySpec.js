describe('session factory test', function () {
    var sessionfactory, session;
    beforeEach(module('DCRA'));
    beforeEach(inject(function (SessionFactory) {
        session = {
            isDirty: false
        };

        sessionfactory = SessionFactory;
        spyOn(sessionfactory, 'getSession').and.returnValue(session);
    }));

    it('should test getSession Method', function () {
        var res=sessionfactory.getSession();
        expect(res).toEqual(session);
    });

    it('should test setSessionAsDirty Method', function () {
        sessionfactory.setSessionAsDirty();
        expect(session.isDirty).toBeTruthy();
    });

    it('should test setSessionAsClear Method', function () {
        sessionfactory.setSessionAsClear();
        expect(session.isDirty).toBeFalsy();
    });

    it('should test isSessionDirty Method', function () {
        expect(sessionfactory.isSessionDirty()).toBeDefined();
    });

});